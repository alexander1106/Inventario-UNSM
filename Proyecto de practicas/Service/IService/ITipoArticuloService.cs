using Proyecto_de_practicas.Modules.Ubicaciones.DTO;

namespace Proyecto_de_practicas.Service
{
    public interface ITipoArticuloService
    {
        Task<List<TipoArticuloDTO>> GetAllAsync();
        Task<TipoArticuloDTO?> GetByIdAsync(int id);
        Task<TipoArticuloDTO> AddAsync(TipoArticuloDTO dto);
        Task<TipoArticuloDTO> UpdateAsync(int id, TipoArticuloDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<TipoArticuloDTO> ObtenerPorIdAsync(int id);
    }
}