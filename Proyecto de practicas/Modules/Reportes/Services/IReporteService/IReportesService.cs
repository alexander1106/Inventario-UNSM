using Proyecto_de_practicas.Modules.Reportes.DTO;

namespace Proyecto_de_practicas.Modules.Reportes.Services.IReporteService
{
    public interface IReportesService
    {
        Task<List<ArticulosPorUbicacionDto>> GetArticulosPorUbicacionAsync();
        Task<List<ArticulosPorTipoDto>> GetArticulosPorTipoAsync();
    }
}
