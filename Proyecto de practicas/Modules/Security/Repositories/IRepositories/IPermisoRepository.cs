using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface IPermisoRepository
    {
        Task<Permiso?> GetByIdAsync(int id);
        Task<IEnumerable<Permiso>> GetAllAsync();
        Task AddAsync(Permiso entity);
        Task UpdateAsync(Permiso entity);
        Task DeleteAsync(int id);
    }
}
