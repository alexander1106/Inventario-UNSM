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

namespace Proyecto_de_practicas.Modules.Articulos.Repository
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly AplicationDBContext _context;

        public ArticuloRepository(AplicationDBContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los artículos (Entidad base)
        public async Task<List<Articulo>> GetAllAsync() =>
            await _context.Articulos.ToListAsync();

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

            // Mapear al DTO del artículo incluyendo los campos nuevos
            var articuloDto = new ArticuloDto
            {
                Id = articulo.Id,
                QRCodeBase64 = articulo.QRCodeBase64,
                CodigoPatrimonial = articulo.CodigoPatrimonial,
                Nombre = articulo.Nombre,
                FechaAdquision = articulo.FechaAdquision,
                ValorAdquisitivo = (decimal)articulo.ValorAdquisitivo, // Casteo seguro a decimal
                Condicion = articulo.Condicion,
                TipoArticuloId = articulo.TipoArticuloId,
                UbicacionId = articulo.UbicacionId,
                VidaUtil = articulo.VidaUtil,
                Estado = articulo.Estado,

                // ✨ Nuevos campos asignados al DTO
                CodigoBarra = articulo.CodigoBarra,
                Marca = articulo.Marca,
                Modelo = articulo.Modelo,
                NroSerie = articulo.NroSerie,
                Medidas = articulo.Medidas,
                Color = articulo.Color,
                Mayor = articulo.Mayor,
                SubCta = articulo.SubCta,
                HValorInicial = articulo.HValorInicial,
                HDeprInicial = articulo.HDeprInicial,
                HDeprAjustada = articulo.HDeprAjustada,
                HDeprEjercicio = articulo.HDeprEjercicio,
                ValorNeto = articulo.ValorNeto,

                CamposValores = camposValores
            };

            return articuloDto;
        }

        // 🔹 Obtener artículo por ID (solo entidad)
        public async Task<Articulo?> GetByIdAsync(int id) =>
            await _context.Articulos.FirstOrDefaultAsync(x => x.Id == id);

        // 🔹 Obtener artículo por QR
        public async Task<Articulo?> GetByCodigoCortoAsync(string codigoCorto) =>
            await _context.Articulos.FirstOrDefaultAsync(a => a.QRCodeBase64 == codigoCorto);

        // 🔹 Agregar artículo (Entidad básica)
        public async Task<Articulo> AddAsync(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        // 🔹 Actualizar artículo mediante Stored Procedure con Campos Dinámicos e Historiales
        public async Task<string> UpdateArticuloConCampos(int id, ArticuloConCamposRequest request)
        {
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
                command.Parameters.Add(new SqlParameter("@QRCodeBase64", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CodigoPatrimonial", request.CodigoPatrimonial ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Nombre", request.Nombre ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@FechaAdquision", request.FechaAdquision));
                command.Parameters.Add(new SqlParameter("@ValorAdquisitivo", request.ValorAdquisitivo));
                command.Parameters.Add(new SqlParameter("@Condicion", request.Condicion ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TipoArticuloId", request.TipoArticuloId));
                command.Parameters.Add(new SqlParameter("@UbicacionId", request.UbicacionId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Estado", request.Estado));
                command.Parameters.Add(new SqlParameter("@VidaUtil", request.VidaUtil ?? (object)DBNull.Value));

                // ✨ Parámetros de los nuevos campos agregados al SP de actualización
                command.Parameters.Add(new SqlParameter("@CodigoBarra", request.CodigoBarra ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Marca", request.Marca ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Modelo", request.Modelo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NroSerie", request.NroSerie ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Medidas", request.Medidas ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Color", request.Color ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Mayor", request.Mayor ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SubCta", request.SubCta ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@HValorInicial", request.HValorInicial));
                command.Parameters.Add(new SqlParameter("@HDeprInicial", request.HDeprInicial));
                command.Parameters.Add(new SqlParameter("@HDeprAjustada", request.HDeprAjustada));
                command.Parameters.Add(new SqlParameter("@HDeprEjercicio", request.HDeprEjercicio));
                command.Parameters.Add(new SqlParameter("@ValorNeto", request.ValorNeto));

                command.Parameters.Add(new SqlParameter("@CamposJSON", camposJson));

                await command.ExecuteNonQueryAsync();
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }

            return "Artículo actualizado correctamente con campos dinámicos";
        }

        // 🔹 Actualizar artículo (Entidad básica)
        public async Task<Articulo> UpdateAsync(Articulo articulo)
        {
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

                command.Parameters.Add(new SqlParameter("@QRCodeBase64", ""));
                command.Parameters.Add(new SqlParameter("@CodigoPatrimonial", request.CodigoPatrimonial ?? ""));
                command.Parameters.Add(new SqlParameter("@Nombre", request.Nombre ?? ""));
                command.Parameters.Add(new SqlParameter("@FechaAdquision", request.FechaAdquision));
                command.Parameters.Add(new SqlParameter("@ValorAdquisitivo", request.ValorAdquisitivo));
                command.Parameters.Add(new SqlParameter("@Condicion", request.Condicion ?? ""));
                command.Parameters.Add(new SqlParameter("@TipoArticuloId", request.TipoArticuloId));
                command.Parameters.Add(new SqlParameter("@UbicacionId", request.UbicacionId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Estado", request.Estado));
                command.Parameters.Add(new SqlParameter("@VidaUtil", request.VidaUtil ?? (object)DBNull.Value));

                // ✨ Parámetros de los nuevos campos agregados al SP de guardado
                command.Parameters.Add(new SqlParameter("@CodigoBarra", request.CodigoBarra ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Marca", request.Marca ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Modelo", request.Modelo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NroSerie", request.NroSerie ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Medidas", request.Medidas ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Color", request.Color ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Mayor", request.Mayor ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SubCta", request.SubCta ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@HValorInicial", request.HValorInicial));
                command.Parameters.Add(new SqlParameter("@HDeprInicial", request.HDeprInicial));
                command.Parameters.Add(new SqlParameter("@HDeprAjustada", request.HDeprAjustada));
                command.Parameters.Add(new SqlParameter("@HDeprEjercicio", request.HDeprEjercicio));
                command.Parameters.Add(new SqlParameter("@ValorNeto", request.ValorNeto));

                command.Parameters.Add(new SqlParameter("@CamposJSON", camposJson));

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
                articulo.QRCodeBase64 = GenerarUrlAngular(articulo.Id);
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
                    QRCodeBase64 = articulo.QRCodeBase64,
                    CodigoPatrimonial = articulo.CodigoPatrimonial,
                    Nombre = articulo.Nombre,
                    FechaAdquision = articulo.FechaAdquision,
                    ValorAdquisitivo = (decimal)articulo.ValorAdquisitivo,
                    Condicion = articulo.Condicion,
                    TipoArticuloId = articulo.TipoArticuloId,
                    UbicacionId = articulo.UbicacionId,
                    VidaUtil = articulo.VidaUtil,
                    Estado = articulo.Estado,

                    // ✨ Mapeo masivo en el listado general
                    CodigoBarra = articulo.CodigoBarra,
                    Marca = articulo.Marca,
                    Modelo = articulo.Modelo,
                    NroSerie = articulo.NroSerie,
                    Medidas = articulo.Medidas,
                    Color = articulo.Color,
                    Mayor = articulo.Mayor,
                    SubCta = articulo.SubCta,
                    HValorInicial = articulo.HValorInicial,
                    HDeprInicial = articulo.HDeprInicial,
                    HDeprAjustada = articulo.HDeprAjustada,
                    HDeprEjercicio = articulo.HDeprEjercicio,
                    ValorNeto = articulo.ValorNeto,

                    CamposValores = camposValores
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
                        FechaAdquision = DateTime.Now, // O mapear si viene en el Excel
                        Condicion = fila.ContainsKey("Condicion") ? fila["Condicion"]?.ToString() : "Usado",
                        TipoArticuloId = fila.ContainsKey("TipoId") ? Convert.ToInt32(fila["TipoId"]) : 1, // Tipo por defecto
                        Estado = 1,

                        // 🔥 AQUÍ LA MAGIA: Si el Excel no trae Ubicación, le encajamos el ID 100 directo
                        UbicacionId = fila.ContainsKey("UbicacionId") && fila["UbicacionId"] != null
                            ? Convert.ToInt32(fila["UbicacionId"])
                            : 100,

                        // Campos contables que agregaste antes
                        Marca = fila.ContainsKey("Marca") ? fila["Marca"]?.ToString() : null,
                        Modelo = fila.ContainsKey("Modelo") ? fila["Modelo"]?.ToString() : null,
                        NroSerie = fila.ContainsKey("NroSerie") ? fila["NroSerie"]?.ToString() : null,
                        CamposValores = new List<CampoValorDto>() // Lista vacía gracias a tu cambio anterior de permitir nulos/vacíos 🙌
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
    }
}