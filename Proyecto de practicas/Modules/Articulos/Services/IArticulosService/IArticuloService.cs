using Proyecto_de_practicas.Modules.Articulos.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_de_practicas.Service
{
    public interface IArticuloService
    {
        Task<List<ArticuloDto>> GetAllAsync();
        Task<ArticuloDto?> GetByIdAsync(int id);
        Task<ArticuloDto> AddAsync(ArticuloDto dto);
        Task<ArticuloDto> UpdateAsync(int id, ArticuloDto dto);

        // 🔹 Métodos para campos dinámicos
        Task<List<CampoArticuloDto>> GetCamposPorTipoArticuloAsync(int tipoArticuloId);

        // 🔹 Nuevo: obtener todos con campos
        Task<List<ArticuloDto>> GetAllConCamposAsync();

        Task<bool> DeleteAsync(int id);
        Task<List<ArticuloDto>> GetByTipoArticuloIdAsync(int tipoArticuloId);
        Task<List<ArticuloDto>> GetByUbicacionIdAsync(int ubicacionId);
        Task<string> GuardarArticuloConCampos(ArticuloConCamposRequest request);
        Task<List<Dictionary<string, object>>> GetArticulosPivotPorTipoAsync(int tipoArticuloId);
        Task<ArticuloDto?> GetByCodigoCortoAsync(string codigoCorto);

        // 🔹 Método agregado para campos dinámicos
    }
}
