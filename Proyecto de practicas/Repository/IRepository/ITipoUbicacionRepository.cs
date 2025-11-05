using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository.IRepository
{
    public interface ITipoUbicacionRepository
    {
        Task<List<TipoUbicacion>> GetAllAsync();
        Task<TipoUbicacion?> GetByIdAsync(int id);
        Task<TipoUbicacion> AddAsync(TipoUbicacion tipoubicacion);
        Task<TipoUbicacion> UpdateAsync(TipoUbicacion tipoubicacion);
        Task<bool> DeleteAsync(int id);
    }
}
