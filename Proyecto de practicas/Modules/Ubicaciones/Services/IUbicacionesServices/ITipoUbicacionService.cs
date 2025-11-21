 
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices
{
    public interface ITipoUbicacionService
    {
        Task<List<TipoUbicacion>> GetAllAsync();
        Task<TipoUbicacion?> GetByIdAsync(int id);
        Task<TipoUbicacion> AddAsync(TipoUbicacion tipoUbicacion);
        Task<TipoUbicacion> UpdateAsync(int id, TipoUbicacion tipoUbicacion);
        Task<bool> DeleteAsync(int id);
    }
}
