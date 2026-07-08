using Proyecto_de_practicas.Modules.Articulos.DTO;

namespace Proyecto_de_practicas.Modules.Articulos.Services
{
    public interface IClasificacionDepreciacionService
    {
        Task<List<ClasificacionDepreciacionDto>> GetAllAsync();
        Task<ClasificacionDepreciacionDto?> GetByIdAsync(int id);
        Task<ClasificacionDepreciacionDto> AddAsync(ClasificacionDepreciacionDto dto);
        Task<ClasificacionDepreciacionDto> UpdateAsync(int id, ClasificacionDepreciacionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
