using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface ITipoArticuloRepository
    {
        Task<List<TipoArticulo>> GetAllAsync();
        Task<TipoArticulo?> GetByIdAsync(int id);
        Task<TipoArticulo> AddAsync(TipoArticulo tipoArticulo);
        Task<TipoArticulo> UpdateAsync(TipoArticulo tipoArticulo);
        Task<bool> DeleteAsync(int id);
        Task<TipoArticulo> GetByIdWithArticulosAsync(int id);
    }
}