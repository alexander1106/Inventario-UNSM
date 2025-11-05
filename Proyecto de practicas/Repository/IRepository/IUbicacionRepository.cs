using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository.IRepository
{
    public interface IUbicacionRepository
    {
        Task<List<Ubicacion>> GetAllAsync();
        Task<Ubicacion?> GetByIdAsync(int id);
        Task<Ubicacion> AddAsync(Ubicacion ubicacion);
        Task<Ubicacion> UpdateAsync(Ubicacion ubicacion);
        Task<bool> DeleteAsync(int id);
    }
}

    

