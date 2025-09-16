using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface IFacultadesService
    {
        Task<List<FacultadesDto>> GetListFacultades();
        Task<FacultadesDto?> GetFacultades(int id);
        Task<FacultadesDto> AddFacultades(FacultadesDto facultad);
        Task<FacultadesDto?> ActualizarFacultadAsync(FacultadesDto facultad);
        Task<bool> EliminarFacultadAsync(int id);
    }
}
