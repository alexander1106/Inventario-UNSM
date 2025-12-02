using Proyecto_de_practicas.Modules.Security.DTO;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface IPermisoService
    {
        Task<PermisoDto?> GetAsync(int id);
        Task<IEnumerable<PermisoDto>> GetAllAsync();
        Task<PermisoDto> CreateAsync(PermisoDto dto);
        Task<PermisoDto?> UpdateAsync(PermisoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}