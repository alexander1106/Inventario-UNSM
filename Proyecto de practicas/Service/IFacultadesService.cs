using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface IFacultadesService
    {
        Task<List<Facultades>> GetListFacultades();
        Task<Facultades?> GetFacultades(int id);
        Task<Facultades> AddFacultades(Facultades facultad);
        Task<Facultades?> ActualizarFacultadAsync(Facultades facultad);
        Task<bool> EliminarFacultadAsync(int id);
    }
}
