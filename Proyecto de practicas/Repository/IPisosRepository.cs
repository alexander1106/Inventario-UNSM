using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface IPisosRepository
    {
        Task<List<Pisos>> GetAllAsync();
        Task<Pisos?> GetByIdAsync(int id);
        Task<Pisos?> GetByNumeroAsync(int numero);
        Task<Pisos> AddAsync(Pisos piso);
        Task<Pisos> UpdateAsync(Pisos piso);
        Task<bool> DeleteAsync(int id);
    }
}
