using Proyecto_de_practicas.Modules.Security.Security;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface IRolSubModuloRepository
    {
        Task<RolSubModulo> GetByIdAsync(int rolId, int subModuloId);
        Task<IEnumerable<RolSubModulo>> GetByRolAsync(int rolId);
        Task AddAsync(RolSubModulo entity);
        Task UpdateAsync(RolSubModulo entity);
        Task DeleteAsync(int rolId, int subModuloId);

        Task DeleteByRolIdAsync(int rolId);

        // Agregar varios submódulos a la vez
        Task AddRangeAsync(List<RolSubModulo> list);
    }
}
