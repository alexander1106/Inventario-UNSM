using Proyecto_de_practicas.Modules.Security.DTO;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface IRolSubModuloPermisoService
    {
        Task<RolSubModuloPermisoDto?> GetAsync(int id);
        Task<IEnumerable<RolSubModuloPermisoDto>> GetByRolSubModuloAsync(int rolSubModuloId);
        Task<RolSubModuloPermisoDto> CreateAsync(int rolSubModuloId, int permisoId);
        Task<RolSubModuloPermisoDto?> UpdateAsync(RolSubModuloPermisoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}