using Proyecto_de_practicas.Modules.Prestamos.DTO;

namespace Proyecto_de_practicas.Modules.Prestamos.Services.IServices
{
    public interface IServicePrestamos
    {
        Task<IEnumerable<PrestamoDTO>> GetAllAsync();
        Task<PrestamoDTO?> GetByIdAsync(int id);
        Task<PrestamoDTO> CreateAsync(CreatePrestamoDTO request);
        Task<PrestamoDTO?> UpdateAsync(int id, UpdatePrestamosDTO request);
        Task<bool> DeleteAsync(int id);
        Task<PrestamoDTO?> UpdateEstadoPrestamoAsync(int id, bool nuevoEstado);
        Task<IEnumerable<PrestamoDTO>> GetPrestamosActivosAsync();
        Task<bool> ExistePrestamoAsync(int id);
        Task<int> CountAsync();
        Task<string> UploadPdfAsync(int prestamoId, IFormFile file);
        Task<PrestamoDTO?> CambiarEstadoAsync(int id, bool estado);
        Task<IEnumerable<PrestamoDTO>> GetByUbicacionAsync(int ubicacionId);
    }
}

