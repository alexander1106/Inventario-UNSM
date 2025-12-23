using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.DTO; // Asegúrate de tener el DTO

namespace Proyecto_de_practicas.Modules.Security.Repositories
{
    public class RolSubModuloRepository : IRolSubModuloRepository
    {
        private readonly AplicationDBContext _context;

        public RolSubModuloRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<RolSubModulo?> GetByIdAsync(int rolId, int subModuloId)
        {
            return await _context.RolSubmodulo
                .Include(r => r.Permisos)
                .FirstOrDefaultAsync(r => r.RolId == rolId && r.SubModuloId == subModuloId);
        }

        public async Task<IEnumerable<RolSubModulo>> GetByRolRawAsync(int rolId)
        {
            return await _context.RolSubmodulo
                .Where(r => r.RolId == rolId)
                .Include(r => r.Permisos)
                .ToListAsync();
        }

        public async Task<List<SubModuloDTO>> GetSubModulosByRolAsync(int rolId)
        {
            return await _context.RolSubmodulo
                .Where(rsm => rsm.RolId == rolId)
                .Include(rsm => rsm.SubModulo)
                .Select(rsm => new SubModuloDTO
                {
                    Id = rsm.SubModulo.Id,
                    Nombre = rsm.SubModulo.Nombre,
                    Ruta = rsm.SubModulo.Ruta,
                    Icon = rsm.SubModulo.Icon,
                    ModuloId = rsm.SubModulo.ModuloId  // <-- Esto es clave

                })
                .ToListAsync();
        }

        public async Task AddAsync(RolSubModulo entity)
        {
            _context.RolSubmodulo.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<RolSubModulo> list)
        {
            await _context.RolSubmodulo.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RolSubModulo entity)
        {
            _context.RolSubmodulo.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int rolId, int subModuloId)
        {
            var item = await GetByIdAsync(rolId, subModuloId);
            if (item != null)
            {
                _context.RolSubmodulo.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByRolIdAsync(int rolId)
        {
            var registros = await _context.RolSubmodulo
                .Where(r => r.RolId == rolId)
                .ToListAsync();

            _context.RolSubmodulo.RemoveRange(registros);
            await _context.SaveChangesAsync();
        }
    }
}