using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository
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
