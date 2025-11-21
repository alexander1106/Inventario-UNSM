using Proyecto_de_practicas.Modules.Security.DTO;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface IModulosService
    {
        Task<IEnumerable<ModuloDTO>> GetAllAsync();
        Task<ModuloDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ModuloDTO>> SearchByNombreAsync(string nombre);
    }
}
