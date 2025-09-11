namespace Proyecto_de_practicas.Service
{
    public interface IRolesService
    {
        Task<List<string>> GetAllRolesAsync();
        Task<bool> RoleExistsAsync(string roleName);
        Task AddRoleAsync(string roleName);
        Task DeleteRoleAsync(string roleName);
    }
}
