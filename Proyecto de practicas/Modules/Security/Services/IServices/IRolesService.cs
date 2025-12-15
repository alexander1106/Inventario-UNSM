
using Proyecto_de_practicas.Modules.Security.DTO;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface IRolesService
    {
        Task<List<RolesDTO>> GetAllRolesAsync();
        Task<RolesDTO?> GetByIdAsync(int id);
        Task<RolesDTO?> GetByNombreAsync(string nombre);
        Task<bool> RoleExistsAsync(string nombre);
        Task<RolesDTO> AddRoleAsync(RolesDTO rol);
        Task<RolesDTO> UpdateRoleAsync(RolesDTO rol);
        Task<bool> DeleteRol(int id);
    }
}
