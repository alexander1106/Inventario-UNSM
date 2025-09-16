using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface IRolesService
    {
        Task<List<RolesDTO>> GetAllRolesAsync();
        Task<bool> RoleExistsAsync(string roleName);
        Task<RolesDTO> AddRoleAsync(RolesDTO roles);
        Task<bool> DeleteRol(int id);
    }
}
