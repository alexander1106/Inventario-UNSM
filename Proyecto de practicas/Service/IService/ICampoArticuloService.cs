using Proyecto_de_practicas.DTO;

namespace Proyecto_de_practicas.Service
{
    public interface ICampoArticuloService
    {
        Task<List<CampoArticuloDto>> GetAllAsync();
        Task<CampoArticuloDto?> GetByIdAsync(int id);
        Task<CampoArticuloDto> AddAsync(CampoArticuloDto dto);
        Task<CampoArticuloDto> UpdateAsync(int id, CampoArticuloDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<CampoArticuloDto>> GetByTipoArticuloIdAsync(int tipoArticuloId);
    }
}