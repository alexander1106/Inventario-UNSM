using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;

namespace Proyecto_de_practicas.Modules.Security.Repositories
{
    public class RolSubModuloPermisoRepository:IRolSubModuloPermisoRepository
    {
        private readonly AplicationDBContext _context;

        public RolSubModuloPermisoRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<RolSubModuloPermiso?> GetByIdAsync(int id)
        {
            return await _context.RolSubModuloPermisos.FindAsync(id);
        }

        public async Task<IEnumerable<RolSubModuloPermiso>> GetByRolSubModuloAsync(int rolSubModuloId)
        {
            return await _context.RolSubModuloPermisos
                                 .Where(r => r.RolSubModuloId == rolSubModuloId)
                                 .ToListAsync();
        }

        public async Task AddAsync(RolSubModuloPermiso entity)
        {
            _context.RolSubModuloPermisos.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RolSubModuloPermiso entity)
        {
            _context.RolSubModuloPermisos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.RolSubModuloPermisos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}