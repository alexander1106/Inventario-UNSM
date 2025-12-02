using Proyecto_de_practicas.Modules.Articulos.Entities;

namespace Proyecto_de_practicas.Repository.IRepository
{
    public interface ICampoArticuloRepository
    {
        Task<List<CampoArticulo>> GetAllAsync();
        Task<CampoArticulo?> GetByIdAsync(int id);
        Task<CampoArticulo> AddAsync(CampoArticulo campoArticulo);
        Task<CampoArticulo> UpdateAsync(CampoArticulo campoArticulo);
        Task<bool> DeleteAsync(int id);
        Task<List<CampoArticulo>> GetByTipoArticuloIdAsync(int tipoArticuloId);

        Task<bool> ExistsDuplicateAsync(string nombreCampo, int tipoArticuloId, int? excludeId = null);
         Task<bool> HasRelationsAsync(int campoArticuloId);


    }
}
