using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface IArticuloCampoValorService
    {
        Task<IEnumerable<ArticuloCampoValorDto>> GetAllAsync();
        Task<ArticuloCampoValorDto?> GetByIdAsync(int id);
        Task<IEnumerable<ArticuloCampoValorDto>> GetByArticuloIdAsync(int articuloId);
        Task AddAsync(ArticuloCampoValorDto dto);
        Task UpdateAsync(ArticuloCampoValorDto dto);
        Task DeleteAsync(int id);
        // Nuevo método para filtrar por TipoArticuloId
        Task<IEnumerable<ArticuloCampoValorDto>> GetByTipoArticuloIdAsync(int tipoArticuloId);


    }
}