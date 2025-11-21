using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface IRolesRepository
    {
        Task<List<Roles>> GetAllAsync();
        Task<Roles?> GetByIdAsync(int id);
        Task<Roles?> GetByNombreAsync(string nombre);
        Task<Roles> AddAsync(Roles rol);
        Task<Roles> UpdateAsync(Roles rol);
        Task<bool> DeleteAsync(int id);

    }
}
