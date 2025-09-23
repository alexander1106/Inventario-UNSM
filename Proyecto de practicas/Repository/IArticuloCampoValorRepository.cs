using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface IArticuloCampoValorRepository
    {
        Task<IEnumerable<ArticuloCampoValor>> GetAllAsync();
        Task<ArticuloCampoValor?> GetByIdAsync(int id);
        Task<IEnumerable<ArticuloCampoValor>> GetByArticuloIdAsync(int articuloId);
        Task AddAsync(ArticuloCampoValor entity);
        Task UpdateAsync(ArticuloCampoValor entity);
        Task DeleteAsync(int id);
    }
}