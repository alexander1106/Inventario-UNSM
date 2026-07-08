using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;

namespace Proyecto_de_practicas.Modules.Articulos.Repository
{
    public class ClasificacionDepreciacionRepository : IClasificacionDepreciacionRepository
    {
        private readonly AplicationDBContext _context;

        public ClasificacionDepreciacionRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ClasificacionDepreciacion>> GetAllAsync()
        {
            return await _context.ClasificacionesDepreciacion
                .Where(c => c.Estado == 1)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        public async Task<ClasificacionDepreciacion?> GetByIdAsync(int id)
        {
            return await _context.ClasificacionesDepreciacion
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ClasificacionDepreciacion> AddAsync(ClasificacionDepreciacion entity)
        {
            _context.ClasificacionesDepreciacion.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ClasificacionDepreciacion> UpdateAsync(ClasificacionDepreciacion entity)
        {
            _context.ClasificacionesDepreciacion.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ClasificacionesDepreciacion.FindAsync(id);
            if (entity == null)
                return false;

            entity.Estado = 0;
            _context.ClasificacionesDepreciacion.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteNombreAsync(string nombre, int? excluirId = null)
        {
            var query = _context.ClasificacionesDepreciacion
                .Where(c => c.Nombre.ToLower() == nombre.ToLower());

            if (excluirId.HasValue)
                query = query.Where(c => c.Id != excluirId.Value);

            return await query.AnyAsync();
        }

        public async Task<bool> TieneRelacionConArticulosAsync(int id)
        {
            return await _context.Articulos.AnyAsync(a => a.ClasificacionDepreciacionId == id);
        }
    }
}
