using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;

namespace Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository
{
    public interface IArticuloRepository
    {
        Task<List<Articulo>> GetAllAsync();
        Task<Articulo?> GetByIdAsync(int id);
        Task<Articulo> AddAsync(Articulo articulo);
        Task<Articulo> UpdateAsync(Articulo articulo);
        Task<bool> DeleteAsync(int id);
        Task<List<Articulo>> GetByTipoArticuloIdAsync(int tipoArticuloId);
        Task<List<Articulo>> GetByUbicacionIdAsync(int ubicacionId);

        Task<string> CreateArticuloConCampos(ArticuloDto request);
    }
}