using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface ISubModulosService
    {
        Task<IEnumerable<SubModuloDTO>> GetAllAsync();
        Task<SubModuloDTO> GetByIdAsync(int id);
        Task<IEnumerable<SubModuloDTO>> GetByModuloIdAsync(int moduloId);
        Task<IEnumerable<SubModuloDTO>> SearchByNombreAsync(string nombre);
    }
}
