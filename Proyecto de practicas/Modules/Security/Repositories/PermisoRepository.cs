using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;

namespace Proyecto_de_practicas.Modules.Security.Repositories
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly AplicationDBContext _context;

        public PermisoRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Permiso?> GetByIdAsync(int id)
        {
            return await _context.Permisos.FindAsync(id);
        }

        public async Task<IEnumerable<Permiso>> GetAllAsync()
        {
            return await _context.Permisos.ToListAsync();
        }

        public async Task AddAsync(Permiso entity)
        {
            _context.Permisos.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permiso entity)
        {
            _context.Permisos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Permisos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
