using Proyecto_de_practicas.Modules.Reportes.DTO;

namespace Proyecto_de_practicas.Modules.Reportes.Repository.IReporteRepository
{
    public interface IReportesRepository
    {
        Task<List<ArticulosPorUbicacionDto>> GetArticulosPorUbicacionAsync();
        Task<List<ArticulosPorTipoDto>> GetArticulosPorTipoAsync();
    }
}
