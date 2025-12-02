using Proyecto_de_practicas.Modules.Articulos.DTO;

namespace Proyecto_de_practicas.Service
{
    public interface IArticuloService
    {
        Task<List<ArticuloDto>> GetAllAsync();
        Task<ArticuloDto?> GetByIdAsync(int id);
        Task<ArticuloDto> AddAsync(ArticuloDto dto);
        Task<ArticuloDto> UpdateAsync(int id, ArticuloDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<ArticuloDto>> GetByTipoArticuloIdAsync(int tipoArticuloId);
        Task<List<ArticuloDto>> GetByUbicacionIdAsync(int ubicacionId);
        Task<string> CreateArticuloConCampos(ArticuloDto request);

    }
}