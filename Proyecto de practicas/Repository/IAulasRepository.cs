using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface IAulasRepository
    {
        Task<List<Aulas>> GetAllAsync();
        Task<Aulas?> GetByIdAsync(int id);
        Task<Aulas?> GetByNombreAsync(string nombre);
        Task<Aulas> AddAsync(Aulas aulas);
        Task<Aulas> UpdateAsync(Aulas aulas);
        Task<bool> DeleteAsync(int id);
    }
}
