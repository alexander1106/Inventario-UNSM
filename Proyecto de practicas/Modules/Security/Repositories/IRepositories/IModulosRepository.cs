using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface IModulosRepository
    {
        Task<IEnumerable<Modulo>> GetAllModulosAsync();
        Task<Modulo?> GetModuloByIdAsync(int id);
        Task<IEnumerable<Modulo>> SearchModulosByNombreAsync(string nombre);
    }
}