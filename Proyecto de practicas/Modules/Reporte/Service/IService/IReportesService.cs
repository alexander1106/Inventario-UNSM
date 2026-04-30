using Proyecto_de_practicas.Modules.Reporte.DTO;

namespace Proyecto_de_practicas.Modules.Reporte.Service.IService
{
    public interface IReportesService
    {
        Task<ReporteResponseDto> GenerarReporteAsync(ReporteRequestDto filtros);
    }
}
