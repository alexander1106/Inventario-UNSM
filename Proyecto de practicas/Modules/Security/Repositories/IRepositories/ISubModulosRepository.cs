using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface ISubModulosRepository
    {
        Task<IEnumerable<SubModulo>> GetAllSubModulosAsync();
        Task<IEnumerable<SubModulo>> GetSubModulosByModuloIdAsync(int moduloId);
        Task<SubModulo?> GetSubModuloByIdAsync(int id);
        Task<IEnumerable<SubModulo>> SearchSubModulosByNombreAsync(string nombre);
    }
}