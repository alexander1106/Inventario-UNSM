using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository
{
    public interface IArticuloRepository
    {
        // Métodos básicos CRUD
        Task<List<Articulo>> GetAllAsync();
        Task<Articulo?> GetByIdAsync(int id);
        Task<Articulo> AddAsync(Articulo articulo);
        Task<Articulo> UpdateAsync(Articulo articulo);
        Task<bool> DeleteAsync(int id);

        // Métodos por tipo o ubicación
        Task<List<Articulo>> GetByTipoArticuloIdAsync(int tipoArticuloId);
        Task<List<Articulo>> GetByUbicacionIdAsync(int ubicacionId);

        // Métodos para artículos con campos dinámicos
        Task<string> GuardarArticuloConCampos(ArticuloConCamposRequest request);
        Task<List<ArticuloDto>> GetAllConCamposAsync();

        Task<Articulo?> GetByCodigoCortoAsync(string codigoCorto);
        Task<List<CampoArticuloDto>> GetCamposPorTipoArticuloAsync(int tipoArticuloId);

        // Método para pivot/dinámico
        Task<List<Dictionary<string, object>>> GetArticulosPivotPorTipoAsync(int tipoArticuloId);
    }
}
