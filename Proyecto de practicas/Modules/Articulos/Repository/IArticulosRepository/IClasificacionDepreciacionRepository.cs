using Proyecto_de_practicas.Modules.Articulos.Entities;

namespace Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository
{
    public interface IClasificacionDepreciacionRepository
    {
        Task<List<ClasificacionDepreciacion>> GetAllAsync();
        Task<ClasificacionDepreciacion?> GetByIdAsync(int id);
        Task<ClasificacionDepreciacion> AddAsync(ClasificacionDepreciacion entity);
        Task<ClasificacionDepreciacion> UpdateAsync(ClasificacionDepreciacion entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExisteNombreAsync(string nombre, int? excluirId = null);
        Task<bool> TieneRelacionConArticulosAsync(int id);
    }
}
