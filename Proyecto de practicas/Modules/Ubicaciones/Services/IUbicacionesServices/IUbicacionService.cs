using Proyecto_de_practicas.Modules.Ubicaciones.DTO;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices
{
    public interface IUbicacionService
    {
        Task<List<UbicacionDto>> GetAllAsync();
        Task<UbicacionDto?> GetByIdAsync(int id);
        Task<UbicacionDto> AddAsync(UbicacionDto dto);
        Task<UbicacionDto> UpdateAsync(int id, UbicacionDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<UbicacionDto>> GetByTipoAsync(int tipoId);
    }
}