using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Security;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface IRolSubModuloRepository
    {
        Task<RolSubModulo?> GetByIdAsync(int rolId, int subModuloId);
        Task<IEnumerable<RolSubModulo>> GetByRolRawAsync(int rolId); // Para obtener relaciones
        Task<List<SubModuloDTO>> GetSubModulosByRolAsync(int rolId);   // Para Angular

        Task AddAsync(RolSubModulo entity);
        Task AddRangeAsync(List<RolSubModulo> list);
        Task UpdateAsync(RolSubModulo entity);
        Task DeleteAsync(int rolId, int subModuloId);
        Task DeleteByRolIdAsync(int rolId);

    }
}