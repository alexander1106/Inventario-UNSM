using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices
{
    public interface IFacultadesService
    {
        Task<IEnumerable<Facultades>> GetAllAsync();
        Task<Facultades?> GetByIdAsync(int id);
        Task<IEnumerable<Facultades>> GetBySedeIdAsync(int sedeId);

        Task<IEnumerable<FacultadesDetalleDto>> GetAllDetalleAsync();
        Task<FacultadesDetalleDto?> GetDetalleByIdAsync(int id);

        Task<Facultades> CreateAsync(Facultades facultad);
        Task<Facultades> UpdateAsync(int id, Facultades facultad);
        Task DeleteAsync(int id);
    }
}