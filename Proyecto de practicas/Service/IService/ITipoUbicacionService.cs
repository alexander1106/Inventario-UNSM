using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.DTOs;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
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
