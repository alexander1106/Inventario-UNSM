using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface IEquiposService
    {
        Task<List<Equipos>> GetListEquipos();
        Task<Equipos?> GetEquipos(int id);
        Task<Equipos> AddEquipos(Equipos equipo);
        Task<Equipos?> ActualizarEquipoAsync(Equipos equipo);
        Task<bool> EliminarEquipoAsync(int id);
    }
}
