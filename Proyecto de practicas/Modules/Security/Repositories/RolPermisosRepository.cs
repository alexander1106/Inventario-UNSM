using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.DTO;

namespace Proyecto_de_practicas.Modules.Security.Repositories
{
    public class RolPermisosRepository : IRolPermisoRepository
    {
        private readonly AplicationDBContext _context;

        public RolPermisosRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<RolPermisos?> GetByIdAsync(int id)
        {
            return await _context.RolPermisos
                .Include(r => r.Permiso)
                .Include(r => r.SubModulo)
                .Include(r => r.Modulo)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<RolPermisos?> GetByKeysAsync(int rolId, int? moduloId, int? subModuloId, int permisoId)
        {
            return await _context.RolPermisos
                .Include(r => r.Permiso)
                .Include(r => r.SubModulo)
                .Include(r => r.Modulo)
                .FirstOrDefaultAsync(r =>
                    r.RolId == rolId &&
                    r.ModuloId == moduloId &&
                    r.SubModuloId == subModuloId &&
                    r.PermisoId == permisoId);
        }

        public async Task<IEnumerable<RolPermisos>> GetByRolRawAsync(int rolId)
        {
            return await _context.RolPermisos
                .Where(r => r.RolId == rolId)
                .Include(r => r.Permiso)
                .Include(r => r.SubModulo)
                .Include(r => r.Modulo)
                .ToListAsync();
        }

        public async Task<List<SubModuloDTO>> GetSubModulosByRolAsync(int rolId)
        {
            return await _context.RolPermisos
                .Where(r => r.RolId == rolId && r.SubModuloId != null)
                .Include(r => r.SubModulo)
                .Select(r => new SubModuloDTO
                {
                    Id = r.SubModulo.Id,
                    Nombre = r.SubModulo.Nombre,
                    Ruta = r.SubModulo.Ruta,
                    Icon = r.SubModulo.Icon,
                    ModuloId = r.SubModulo.ModuloId
                })
                .Distinct()
                .ToListAsync();
        }

        public async Task AddAsync(RolPermisos entity)
        {
            _context.RolPermisos.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<RolPermisos> list)
        {
            await _context.RolPermisos.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RolPermisos entity)
        {
            _context.RolPermisos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int rolId, int? moduloId, int? subModuloId, int permisoId)
        {
            var item = await GetByKeysAsync(rolId, moduloId, subModuloId, permisoId);

            if (item != null)
            {
                _context.RolPermisos.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(List<RolPermisos> list)
        {
            _context.RolPermisos.RemoveRange(list);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByRolIdAsync(int rolId)
        {
            var registros = await _context.RolPermisos
                .Where(r => r.RolId == rolId)
                .ToListAsync();

            _context.RolPermisos.RemoveRange(registros);
            await _context.SaveChangesAsync();
        }
    }
}