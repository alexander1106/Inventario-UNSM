using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{ 
    public interface IRolSubModuloService
    {
        Task<RolSubModuloDto?> GetAsync(int rolId, int subModuloId);
        Task<IEnumerable<RolSubModuloDto>> GetByRolAsync(int rolId);
        Task<RolSubModuloDto> CreateAsync(int rolId, int subModuloId);
        Task<RolSubModuloDto> UpdateAsync(RolSubModuloDto entity);
        Task<bool> DeleteAsync(int rolId, int subModuloId);
    }
}
