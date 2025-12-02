using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface IRolSubModuloPermisoRepository
    {
        Task<RolSubModuloPermiso?> GetByIdAsync(int id);
        Task<IEnumerable<RolSubModuloPermiso>> GetByRolSubModuloAsync(int rolSubModuloId);
        Task AddAsync(RolSubModuloPermiso entity);
        Task UpdateAsync(RolSubModuloPermiso entity);
        Task DeleteAsync(int id);
    }
}