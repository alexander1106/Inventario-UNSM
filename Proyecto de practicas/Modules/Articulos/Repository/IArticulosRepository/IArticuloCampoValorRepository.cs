using Proyecto_de_practicas.Modules.Articulos.Entities;

namespace Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository
{
    public interface IArticuloCampoValorRepository
    {
        Task<IEnumerable<ArticuloCampoValor>> GetAllAsync();
        Task<ArticuloCampoValor?> GetByIdAsync(int id);
        Task<IEnumerable<ArticuloCampoValor>> GetByArticuloIdAsync(int articuloId);
        Task AddAsync(ArticuloCampoValor entity);
        Task UpdateAsync(ArticuloCampoValor entity);
        Task<IEnumerable<ArticuloCampoValor>> GetByTipoArticuloIdAsync(int tipoArticuloId);
        Task DeleteAsync(int id);

    }
}