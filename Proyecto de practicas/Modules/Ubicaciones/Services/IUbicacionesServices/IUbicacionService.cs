using Proyecto_de_practicas.Modules.Ubicaciones.DTO;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices
{
    public interface IUbicacionService
    {
        Task<List<UbicacionDto>> GetAllAsync();
        Task<UbicacionDto?> GetByIdAsync(int id);
        Task<List<UbicacionDto>> GetByUsuarioAsync(int usuarioId);
        Task<bool> DeleteAsync(int id);
        Task<List<UbicacionDto>> GetByTipoAsync(int tipoId);
        Task<UbicacionDto> AddAsync(UbicacionDto dto);
        Task<UbicacionDto> UpdateAsync(int id, UbicacionDto dto);
        Task<UbicacionDto> AsignarUsuarioAsync(int ubicacionId, int usuarioId);
        Task<List<UbicacionDto>> GetByPadreAsync(int padreId);
        Task<List<UbicacionDto>> GetByEscuelaIdAsync(int escuelaId);
    }
}