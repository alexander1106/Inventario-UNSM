using Proyecto_de_practicas.Modules.Reporte.DTO;

namespace Proyecto_de_practicas.Modules.Reporte.Repository.IRepository
{
    public interface IReporteRepository
    {
        Task<ReporteResponseDto> ObtenerReporteAsync(ReporteRequestDto filtros);
    }
}
