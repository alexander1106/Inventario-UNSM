using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface IEquiposRepository
    {
        Task<List<Equipos>> GetAllAsync();
        Task<Equipos?> GetByIdAsync(int id);
        Task<Equipos?> GetByNombreAsync(string nombre);
        Task<Equipos> AddAsync(Equipos equipo);
        Task<Equipos> UpdateAsync(Equipos equipo);
        Task<bool> DeleteAsync(int id);
    }
}

