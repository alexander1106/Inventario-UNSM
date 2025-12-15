using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Proyecto_de_practicas.Modules.Articulos.Repository
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly AplicationDBContext _context;

        public ArticuloRepository(AplicationDBContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los artículos
        public async Task<List<Articulo>> GetAllAsync() =>
            await _context.Articulos.ToListAsync();

        // 🔹 Obtener artículo por ID (con campos dinámicos)
        public async Task<ArticuloDto?> GetByIdConCamposAsync(int id)
        {
            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Id == id);
            if (articulo == null) return null;

            // Traer los valores de campos
            var camposValores = await _context.ArticuloCamposValores
                .Where(cv => cv.ArticuloId == id)
                .Select(cv => new ArticuloCampoValorDto
                {
                    Id = cv.Id,
                    ArticuloId = cv.ArticuloId,
                    CampoArticuloId = cv.CampoArticuloId,
                    Valor = cv.Valor
                }).ToListAsync();

            // Mapear al DTO del artículo
            var articuloDto = new ArticuloDto
            {
                Id = articulo.Id,
                QRCodeBase64 = articulo.QRCodeBase64,
                CodigoPatrimonial = articulo.CodigoPatrimonial,
                Nombre = articulo.Nombre,
                FechaAdquision = articulo.FechaAdquision,
                ValorAdquisitivo = articulo.ValorAdquisitivo,
                Condicion = articulo.Condicion,
                TipoArticuloId = articulo.TipoArticuloId,
                UbicacionId = articulo.UbicacionId,
                VidaUtil = articulo.VidaUtil,
                Estado = articulo.Estado,
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

        // 🔹 Agregar artículo
        public async Task<Articulo> AddAsync(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        // 🔹 Actualizar artículo
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

        // 🔹 Guardar artículo con campos dinámicos
        public async Task<string> GuardarArticuloConCampos(ArticuloConCamposRequest request)
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
                command.CommandText = "sp_GuardarArticuloConCampos";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@QRCodeBase64", ""));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@CodigoPatrimonial", request.CodigoPatrimonial));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Nombre", request.Nombre));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@FechaAdquision", request.FechaAdquision));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@ValorAdquisitivo", request.ValorAdquisitivo));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Condicion", request.Condicion));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@TipoArticuloId", request.TipoArticuloId));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@UbicacionId", request.UbicacionId));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Estado", request.Estado));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@VidaUtil", request.VidaUtil));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@CamposJSON", camposJson));

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
                // Generamos la URL para Angular
                articulo.QRCodeBase64 = GenerarUrlAngular(articulo.Id);
                _context.Articulos.Update(articulo);
                await _context.SaveChangesAsync();
            }

            return "Artículo guardado correctamente con QR URL.";
        }

        // 🔹 Generar URL completa para Angular
        private string GenerarUrlAngular(int articuloId)
        {
            string baseUrl = "http://localhost:4200/tipos-articulos/articulo/";
            return baseUrl + articuloId;
        }

        private string GenerarCodigoCorto(string codigoPatrimonial, int articuloId)
        {
            codigoPatrimonial = codigoPatrimonial.Length > 6
                ? codigoPatrimonial[^6..]
                : codigoPatrimonial.PadLeft(6, '0');

            string idBase36 = ConvertToBase36(articuloId).PadLeft(4, '0');
            return codigoPatrimonial + idBase36;
        }

        private string ConvertToBase36(int value)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = "";
            while (value > 0)
            {
                result = chars[value % 36] + result;
                value /= 36;
            }
            return string.IsNullOrEmpty(result) ? "0" : result;
        }
        // 🔹 Obtener todos los artículos con sus campos dinámicos
        public async Task<List<ArticuloDto>> GetAllConCamposAsync()
        {
            // Traer todos los artículos
            var articulos = await _context.Articulos.ToListAsync();
            var result = new List<ArticuloDto>();

            foreach (var articulo in articulos)
            {
                // Traer los valores de los campos para cada artículo
                var camposValores = await _context.ArticuloCamposValores
                    .Where(cv => cv.ArticuloId == articulo.Id)
                    .Select(cv => new ArticuloCampoValorDto
                    {
                        Id = cv.Id,
                        ArticuloId = cv.ArticuloId,
                        CampoArticuloId = cv.CampoArticuloId,
                        Valor = cv.Valor
                    }).ToListAsync();

                // Mapear al DTO del artículo incluyendo campos
                result.Add(new ArticuloDto
                {
                    Id = articulo.Id,
                    QRCodeBase64 = articulo.QRCodeBase64,
                    CodigoPatrimonial = articulo.CodigoPatrimonial,
                    Nombre = articulo.Nombre,
                    FechaAdquision = articulo.FechaAdquision,
                    ValorAdquisitivo = articulo.ValorAdquisitivo,
                    Condicion = articulo.Condicion,
                    TipoArticuloId = articulo.TipoArticuloId,
                    UbicacionId = articulo.UbicacionId,
                    VidaUtil = articulo.VidaUtil,
                    Estado = articulo.Estado,
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
    }
}
