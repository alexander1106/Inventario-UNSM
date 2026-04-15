using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Security;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface IRolPermisoRepository
    {
        Task<RolPermisos?> GetByIdAsync(int id);
        Task<RolPermisos?> GetByKeysAsync(int rolId, int subModuloId, int permisoId);
        Task<IEnumerable<RolPermisos>> GetByRolRawAsync(int rolId);
        Task<List<SubModuloDTO>> GetSubModulosByRolAsync(int rolId);
        Task AddAsync(RolPermisos entity);
        Task AddRangeAsync(List<RolPermisos> list);
        Task UpdateAsync(RolPermisos entity);
        Task DeleteAsync(int rolId, int subModuloId, int permisoId);
        Task DeleteRangeAsync(List<RolPermisos> list);
        Task DeleteByRolIdAsync(int rolId);
    }
}