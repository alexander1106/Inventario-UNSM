using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface IPisosService
    {
        Task<List<Pisos>> GetListPisos();
        Task<Pisos?> GetPisos(int id);
        Task<Pisos> AddPisos(Pisos piso);
        Task<Pisos?> ActualizarPisoAsync(Pisos piso);
        Task<bool> EliminarPisoAsync(int id);
    }
}
