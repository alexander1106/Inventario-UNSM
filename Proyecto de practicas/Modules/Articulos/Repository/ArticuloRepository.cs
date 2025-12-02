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

        public async Task<List<Articulo>> GetAllAsync()
        {
            return await _context.Articulos.ToListAsync();
        }

        public async Task<Articulo?> GetByIdAsync(int id)
        {
            return await _context.Articulos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Articulo> AddAsync(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        public async Task<Articulo> UpdateAsync(Articulo articulo)
        {
            _context.Articulos.Update(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Articulos.FindAsync(id);
            if (entity == null) return false;

            _context.Articulos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Articulo>> GetByTipoArticuloIdAsync(int tipoArticuloId)
        {
            return await _context.Articulos
                .Where(a => a.TipoArticuloId == tipoArticuloId)
                .ToListAsync();
        }

        public async Task<List<Articulo>> GetByUbicacionIdAsync(int ubicacionId)
        {
            return await _context.Articulos
                .Where(a => a.UbicacionId == ubicacionId)
                .ToListAsync();
        }

        // ⭐⭐⭐ ESTE ES EL MÉTODO IMPORTANTE ⭐⭐⭐
        public async Task<string> CreateArticuloConCampos(ArticuloDto request)
        {
            // 1. Crear el artículo
            var articulo = new Articulo
            {
                TipoArticuloId = request.TipoArticuloId,
                UbicacionId = request.UbicacionId,
                Estado = 1
            };

            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync(); // ← Aquí obtiene el ID

            // 2. Guardar sus campos dinámicos
            foreach (var campo in request.CamposValores)
            {
                var entity = new ArticuloCampoValor
                {
                    ArticuloId = articulo.Id,
                    CampoArticuloId = campo.CampoArticuloId,
                    Valor = campo.Valor
                };

                _context.ArticuloCamposValores.Add(entity);
            }

            await _context.SaveChangesAsync();

            return $"Artículo {articulo.Id} creado con {request.CamposValores.Count} campos dinámicos.";
        }
    }
}
