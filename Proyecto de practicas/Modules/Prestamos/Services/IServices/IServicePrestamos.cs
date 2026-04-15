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
        Task<PrestamoDTO?> UpdateEstadoAsync(int id, int nuevoEstado);
        Task<IEnumerable<PrestamoDTO>> GetPrestamosActivosAsync();
        Task<bool> ExistePrestamoAsync(int id);
        Task<int> CountAsync();
    }
}

