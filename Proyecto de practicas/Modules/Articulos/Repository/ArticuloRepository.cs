using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;
using Proyecto_de_practicas.Modules.Articulos.Services;

namespace Proyecto_de_practicas.Modules.Articulos.Repository
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly AplicationDBContext _context;

        public ArticuloRepository(AplicationDBContext context)
        {
            _context = context;
        }

        // 🔹 Si no llega TiempoVidaUtil pero sí una clasificación MEF, copia el valor desde el catálogo
        private async Task<double> ResolverTiempoVidaUtilAsync(double tiempoVidaUtil, int? clasificacionDepreciacionId)
        {
            if (tiempoVidaUtil > 0 || clasificacionDepreciacionId == null)
                return tiempoVidaUtil;

            var vidaUtilAnios = await _context.ClasificacionesDepreciacion
                .Where(c => c.Id == clasificacionDepreciacionId.Value)
                .Select(c => (double?)c.VidaUtilAnios)
                .FirstOrDefaultAsync();

            return vidaUtilAnios ?? tiempoVidaUtil;
        }

        // 🔹 Obtener todos los artículos (Entidad base)
        public async Task<List<Articulo>> GetAllAsync() =>
            await _context.Articulos.ToListAsync();

        // 🔹 Estadísticas generales de artículos (operativos / mantenimiento / baja)
        public async Task<ArticuloEstadisticasDto> GetEstadisticasAsync()
        {
            var totalArticulos = await _context.Articulos.CountAsync();

            var totalOperativos = await _context.Articulos
                .CountAsync(a => a.Condicion != null && a.Condicion.Trim().ToLower() == "operativo");

            var totalBaja = await _context.Articulos
                .CountAsync(a => a.Condicion != null && a.Condicion.Trim().ToLower() == "de baja");

            var totalMantenimiento = await _context.Mantenimientos
                .Where(m => m.Estado && m.EstadoMantenimiento)
                .Select(m => m.ArticuloId)
                .Distinct()
                .CountAsync();

            return new ArticuloEstadisticasDto
            {
                TotalArticulos = totalArticulos,
                TotalOperativos = totalOperativos,
                TotalMantenimiento = totalMantenimiento,
                TotalBaja = totalBaja
            };
        }

        // 🔹 Obtener artículo por ID (con campos dinámicos y nuevos atributos)
        public async Task<ArticuloDto?> GetByIdConCamposAsync(int id)
        {
            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Id == id);
            if (articulo == null) return null;

            // Traer los valores de campos dinámicos
            var camposValores = await _context.ArticuloCamposValores
                .Where(cv => cv.ArticuloId == id)
                .Select(cv => new ArticuloCampoValorDto
                {
                    Id = cv.Id,
                    ArticuloId = cv.ArticuloId,
                    CampoArticuloId = cv.CampoArticuloId,
                    Valor = cv.Valor
                }).ToListAsync();

            var articuloDto = new ArticuloDto
            {
                Id = articulo.Id,
                CodigoPatrimonial = articulo.CodigoPatrimonial,
                CodigoBarra = articulo.CodigoBarra,
                Nombre = articulo.Nombre,
                FechaAdquision = articulo.FechaAdquision,
                ValorAdquisitivo = articulo.ValorAdquisitivo,
                Condicion = articulo.Condicion,
                TipoArticuloId = articulo.TipoArticuloId,
                UbicacionId = articulo.UbicacionId,
                Marca = articulo.Marca,
                Modelo = articulo.Modelo,
                NroSerie = articulo.NroSerie,
                OtrasObservaciones = articulo.OtrasObservaciones,
                TiempoVidaUtil = articulo.TiempoVidaUtil,
                ClasificacionDepreciacionId = articulo.ClasificacionDepreciacionId,
                DepreciacionAnual = articulo.DepreciacionAnual,
                ValorActual = articulo.ValorActual,
                Estado = articulo.Estado
            };

            return articuloDto;
        }

        // 🔹 Obtener artículo por ID (solo entidad)
        public async Task<Articulo?> GetByIdAsync(int id) =>
            await _context.Articulos.FirstOrDefaultAsync(x => x.Id == id);

        // 🔹 Obtener artículo por código de barra (usado para generar/leer el QR en el frontend)
        public async Task<Articulo?> GetByCodigoCortoAsync(string codigoCorto) =>
            await _context.Articulos.FirstOrDefaultAsync(a => a.CodigoBarra == codigoCorto);

        // 🔹 Agregar artículo (Entidad básica)
        public async Task<Articulo> AddAsync(Articulo articulo)
        {
            articulo.ValorActual = DepreciacionCalculator.CalcularValorActual(articulo.ValorAdquisitivo, articulo.FechaAdquision, articulo.DepreciacionAnual);

            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        // 🔹 Actualizar artículo mediante Stored Procedure con Campos Dinámicos e Historiales
        public async Task<string> UpdateArticuloConCampos(int id, ArticuloConCamposRequest request)
        {
            var articuloExistente = await _context.Articulos.FindAsync(id)
                ?? throw new Exception($"No se encontró el artículo con Id {id}");

            var camposJson = System.Text.Json.JsonSerializer.Serialize(
                request.CamposValores.Select(c => new {
                    campo = _context.CamposArticulos
                                    .Where(ca => ca.Id == c.CampoArticuloId)
                                    .Select(ca => ca.NombreCampo)
                                    .FirstOrDefault(),
                    valor = c.Valor
                })
            );

            await _context.Database.OpenConnectionAsync();

            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_UpdateArticuloConCampos";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", id));
                command.Parameters.Add(new SqlParameter("@CodigoPatrimonial", request.CodigoPatrimonial ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Nombre", request.Nombre ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@FechaAdquision", articuloExistente.FechaAdquision));
                command.Parameters.Add(new SqlParameter("@ValorAdquisitivo", request.ValorAdquisitivo));
                command.Parameters.Add(new SqlParameter("@Condicion", request.Condicion ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TipoArticuloId", request.TipoArticuloId));
                command.Parameters.Add(new SqlParameter("@UbicacionId", request.UbicacionId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Estado", articuloExistente.Estado));
                command.Parameters.Add(new SqlParameter("@CodigoBarra", articuloExistente.CodigoBarra ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Marca", request.Marca ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Modelo", request.Modelo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NroSerie", request.NroSerie ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@OtrasObservaciones", request.OtrasObservaciones ?? (object)DBNull.Value));
                double tiempoVidaUtilUpdate = await ResolverTiempoVidaUtilAsync(request.TiempoVidaUtil, request.ClasificacionDepreciacionId);
                double depreciacionUpdate = tiempoVidaUtilUpdate > 0 ? 100.0 / tiempoVidaUtilUpdate : 0;
                command.Parameters.Add(new SqlParameter("@TiempoVidaUtil", tiempoVidaUtilUpdate));
                command.Parameters.Add(new SqlParameter("@ClasificacionDepreciacionId", request.ClasificacionDepreciacionId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DepreciacionAnual", depreciacionUpdate));
                command.Parameters.Add(new SqlParameter("@CamposJSON", camposJson));

                await command.ExecuteNonQueryAsync();
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }

            var articuloActualizado = await _context.Articulos.FirstOrDefaultAsync(a => a.Id == id);
            if (articuloActualizado != null)
            {
                articuloActualizado.ValorActual = DepreciacionCalculator.CalcularValorActual(
                    articuloActualizado.ValorAdquisitivo, articuloActualizado.FechaAdquision, articuloActualizado.DepreciacionAnual);
                _context.Articulos.Update(articuloActualizado);
                await _context.SaveChangesAsync();
            }

            return "Artículo actualizado correctamente con campos dinámicos";
        }

        // 🔹 Actualizar artículo (Entidad básica)
        public async Task<Articulo> UpdateAsync(Articulo articulo)
        {
            articulo.DepreciacionAnual = articulo.TiempoVidaUtil > 0 ? 100.0 / articulo.TiempoVidaUtil : 0;
            articulo.ValorActual = DepreciacionCalculator.CalcularValorActual(articulo.ValorAdquisitivo, articulo.FechaAdquision, articulo.DepreciacionAnual);

            _context.Articulos.Update(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        // 🔹 Eliminar artículo
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Articulos.FindAsync(id);
            if (entity == null) return false;

            _context.Articulos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Obtener artículos por tipo
        public async Task<List<Articulo>> GetByTipoArticuloIdAsync(int tipoArticuloId) =>
            await _context.Articulos
                .Where(a => a.TipoArticuloId == tipoArticuloId)
                .ToListAsync();

        // 🔹 Obtener artículos por ubicación
        public async Task<List<Articulo>> GetByUbicacionIdAsync(int ubicacionId) =>
            await _context.Articulos
                .Where(a => a.UbicacionId == ubicacionId)
                .ToListAsync();

        // 🔹 Guardar artículo con campos dinámicos mediante Stored Procedure
        public async Task<string> GuardarArticuloConCampos(ArticuloConCamposRequest request)
        {
            if (request == null)
                throw new Exception("El request está vacío");

            

            var camposIds = request.CamposValores.Select(c => c.CampoArticuloId).ToList();

            var camposDb = await _context.CamposArticulos
                .Where(ca => camposIds.Contains(ca.Id))
                .ToDictionaryAsync(ca => ca.Id, ca => ca.NombreCampo);

            foreach (var id in camposIds)
            {
                if (!camposDb.ContainsKey(id))
                    throw new Exception($"El CampoArticuloId {id} no existe en la base de datos");
            }

            var camposJson = System.Text.Json.JsonSerializer.Serialize(
                request.CamposValores.Select(c => new
                {
                    campo = camposDb[c.CampoArticuloId],
                    valor = c.Valor
                })
            );

            await _context.Database.OpenConnectionAsync();
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_GuardarArticuloConCampos";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@CodigoPatrimonial", request.CodigoPatrimonial ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Nombre", request.Nombre ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@FechaAdquision", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@ValorAdquisitivo", request.ValorAdquisitivo));
                command.Parameters.Add(new SqlParameter("@Condicion", request.Condicion ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TipoArticuloId", request.TipoArticuloId));
                command.Parameters.Add(new SqlParameter("@UbicacionId", request.UbicacionId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Estado", 1));
                command.Parameters.Add(new SqlParameter("@CodigoBarra", request.CodigoPatrimonial ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Marca", request.Marca ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Modelo", request.Modelo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NroSerie", request.NroSerie ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@OtrasObservaciones", request.OtrasObservaciones ?? (object)DBNull.Value));
                double tiempoVidaUtil = await ResolverTiempoVidaUtilAsync(request.TiempoVidaUtil, request.ClasificacionDepreciacionId);
                command.Parameters.Add(new SqlParameter("@TiempoVidaUtil", tiempoVidaUtil));
                command.Parameters.Add(new SqlParameter("@ClasificacionDepreciacionId", request.ClasificacionDepreciacionId ?? (object)DBNull.Value));
                double depreciacion = tiempoVidaUtil > 0 ? 100.0 / tiempoVidaUtil : 0;
                command.Parameters.Add(new SqlParameter("@DepreciacionAnual", depreciacion));
                command.Parameters.Add(new SqlParameter("@CamposJSON", camposJson));
                command.Parameters.Add(new SqlParameter("@QRCodeBase64", (object)DBNull.Value));

                await command.ExecuteNonQueryAsync();
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }

            var articulo = await _context.Articulos
                .FirstOrDefaultAsync(a => a.CodigoPatrimonial == request.CodigoPatrimonial
                                      && a.TipoArticuloId == request.TipoArticuloId);

            if (articulo != null)
            {
                articulo.ValorActual = DepreciacionCalculator.CalcularValorActual(articulo.ValorAdquisitivo, articulo.FechaAdquision, articulo.DepreciacionAnual);
                _context.Articulos.Update(articulo);
                await _context.SaveChangesAsync();
            }

            return "Artículo guardado correctamente con QR URL.";
        }

        private string GenerarUrlAngular(int articuloId)
        {
            return "http://localhost:4200/tipos-articulos/articulo/" + articuloId;
        }

        // 🔹 Obtener todos los artículos con sus campos dinámicos mapeados
        public async Task<List<ArticuloDto>> GetAllConCamposAsync()
        {
            var articulos = await _context.Articulos.ToListAsync();
            var result = new List<ArticuloDto>();

            foreach (var articulo in articulos)
            {
                var camposValores = await _context.ArticuloCamposValores
                    .Where(cv => cv.ArticuloId == articulo.Id)
                    .Select(cv => new ArticuloCampoValorDto
                    {
                        Id = cv.Id,
                        ArticuloId = cv.ArticuloId,
                        CampoArticuloId = cv.CampoArticuloId,
                        Valor = cv.Valor
                    }).ToListAsync();

                result.Add(new ArticuloDto
                {
                    Id = articulo.Id,
                    CodigoPatrimonial = articulo.CodigoPatrimonial,
                    CodigoBarra = articulo.CodigoBarra,
                    Nombre = articulo.Nombre,
                    FechaAdquision = articulo.FechaAdquision,
                    ValorAdquisitivo = articulo.ValorAdquisitivo,
                    Condicion = articulo.Condicion,
                    TipoArticuloId = articulo.TipoArticuloId,
                    UbicacionId = articulo.UbicacionId,
                    Marca = articulo.Marca,
                    Modelo = articulo.Modelo,
                    NroSerie = articulo.NroSerie,
                    OtrasObservaciones = articulo.OtrasObservaciones,
                    TiempoVidaUtil = articulo.TiempoVidaUtil,
                    ClasificacionDepreciacionId = articulo.ClasificacionDepreciacionId,
                    DepreciacionAnual = articulo.DepreciacionAnual,
                    ValorActual = articulo.ValorActual,
                    Estado = articulo.Estado
                });
            }

            return result;
        }

        // 🔹 Obtener campos por tipo de artículo
        public async Task<List<CampoArticuloDto>> GetCamposPorTipoArticuloAsync(int tipoArticuloId)
        {
            return await _context.CamposArticulos
                .Where(c => c.TipoArticuloId == tipoArticuloId)
                .Select(c => new CampoArticuloDto
                {
                    Id = c.Id,
                    NombreCampo = c.NombreCampo,
                    TipoDato = c.TipoDato,
                    TipoArticuloId = c.TipoArticuloId
                })
                .ToListAsync();
        }

        // 🔹 Obtener artículos pivot por tipo
        public async Task<List<Dictionary<string, object>>> GetArticulosPivotPorTipoAsync(int tipoArticuloId)
        {
            var result = new List<Dictionary<string, object>>();

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "sp_ObtenerArticulosPorTipo";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            var param = command.CreateParameter();
            param.ParameterName = "@TipoArticuloId";
            param.Value = tipoArticuloId;
            command.Parameters.Add(param);

            await _context.Database.OpenConnectionAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);

                result.Add(row);
            }

            return result;
        }

        public async Task<string> ProcesarCargaMasivaExcel(List<object> filas)
        {
            int insertados = 0;
            int errores = 0;

            foreach (IDictionary<string, object> fila in filas)
            {
                try
                {
                    // Mapeas las columnas de tu Excel tal cual tengan los nombres en la primera fila
                    var nuevoArticulo = new ArticuloConCamposRequest
                    {
                        CodigoPatrimonial = fila.ContainsKey("CodigoPatrimonial") ? fila["CodigoPatrimonial"]?.ToString() : "",
                        Nombre = fila.ContainsKey("Nombre") ? fila["Nombre"]?.ToString() : "Artículo Sin Nombre",
                        Condicion = fila.ContainsKey("Condicion") ? fila["Condicion"]?.ToString() : "Usado",
                        TipoArticuloId = fila.ContainsKey("TipoId") ? Convert.ToInt32(fila["TipoId"]) : 1,
                        UbicacionId = fila.ContainsKey("UbicacionId") && fila["UbicacionId"] != null
                            ? Convert.ToInt32(fila["UbicacionId"])
                            : 100,
                        Marca = fila.ContainsKey("Marca") ? fila["Marca"]?.ToString() : null,
                        Modelo = fila.ContainsKey("Modelo") ? fila["Modelo"]?.ToString() : null,
                        NroSerie = fila.ContainsKey("NroSerie") ? fila["NroSerie"]?.ToString() : null,
                        CamposValores = new List<CampoValorDto>()
                    };

                    // Llamas a tu método normal de guardar que ya no te rebota por CamposValores vacíos
                    await GuardarArticuloConCampos(nuevoArticulo);
                    insertados++;
                }
                catch
                {
                    errores++;
                }
            }

            return $"Proceso terminado. Éxito: {insertados} registros. Errores saltados: {errores}.";
        }

        public async Task<List<Articulo>> GetByEscuelaIdAsync(int escuelaId)
        {
            var ubicacionIds = await _context.Ubicaciones
                .Where(u => u.EscuelaId == escuelaId)
                .Select(u => u.Id)
                .ToListAsync();

            return await _context.Articulos
                .Where(a => a.Estado == 1
                         && a.UbicacionId.HasValue
                         && ubicacionIds.Contains(a.UbicacionId.Value))
                .ToListAsync();
        }

        public async Task<bool> ExisteUbicacionAsync(int ubicacionId)
        {
            return await _context.Ubicaciones.AnyAsync(u => u.Id == ubicacionId);
        }
    }
}