using Proyecto_de_practicas.DTOs;

namespace Proyecto_de_practicas.Service
{
    public interface IUbicacionService
    {
        Task<List<UbicacionDto>> GetAllAsync();
        Task<UbicacionDto?> GetByIdAsync(int id);
        Task<UbicacionDto> AddAsync(UbicacionDto dto);
        Task<UbicacionDto> UpdateAsync(int id, UbicacionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}