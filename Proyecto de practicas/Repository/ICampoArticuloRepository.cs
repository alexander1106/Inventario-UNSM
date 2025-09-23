using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface ICampoArticuloRepository
    {
        Task<List<CampoArticulo>> GetAllAsync();
        Task<CampoArticulo?> GetByIdAsync(int id);
        Task<CampoArticulo> AddAsync(CampoArticulo campoArticulo);
        Task<CampoArticulo> UpdateAsync(CampoArticulo campoArticulo);
        Task<bool> DeleteAsync(int id);
        Task<List<CampoArticulo>> GetByTipoArticuloIdAsync(int tipoArticuloId);
    }
}
