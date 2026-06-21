using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices
{
    public interface ISedesService
    {
        Task<IEnumerable<Sedes>> GetAllAsync();
        Task<Sedes?> GetByIdAsync(int id);

        Task<IEnumerable<SedeDetalleDto>> GetAllDetalleAsync();
        Task<SedeDetalleDto?> GetDetalleByIdAsync(int id);

        Task<Sedes> CreateAsync(Sedes sede);
        Task<Sedes> UpdateAsync(int id, Sedes sede);
        Task DeleteAsync(int id);
    }
}