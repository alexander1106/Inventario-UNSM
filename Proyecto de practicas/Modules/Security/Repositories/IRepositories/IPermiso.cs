using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface IPermiso
    {
        Task<Permiso> GetByIdAsync(int id);
        Task<IEnumerable<Permiso>> GetAllAsync();
        Task AddAsync(Permiso permiso);
        Task UpdateAsync(Permiso permiso);
        Task DeleteAsync(int id);
    }
}
