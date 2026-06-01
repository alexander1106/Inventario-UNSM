using Proyecto_de_practicas.Modules.Prestamos.DTO;

namespace Proyecto_de_practicas.Modules.Prestamos.Services.IServices
{
    public interface ISolicitanteService
    {
        Task<List<SolicitanteDto>> GetAllAsync();
        Task<SolicitanteDto?> GetByIdAsync(int id);
        Task<SolicitanteDto> CreateAsync(SolicitanteDto dto);
        Task<bool> UpdateAsync(int id, SolicitanteDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SolicitanteDto>> GetByUsuarioAsync(int usuarioId);

    }
}
