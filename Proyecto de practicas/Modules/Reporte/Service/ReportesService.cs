using Proyecto_de_practicas.Modules.Reporte.DTO;
using Proyecto_de_practicas.Modules.Reporte.Repository.IRepository;
using Proyecto_de_practicas.Modules.Reporte.Service.IService;

namespace Proyecto_de_practicas.Modules.Reporte.Service
{
    public class ReportesService : IReportesService
    {
        private readonly IReporteRepository _repo;
        public ReportesService(IReporteRepository repo) => _repo = repo;

        public async Task<ReporteResponseDto> GenerarReporteAsync(ReporteRequestDto filtros)
        {
            // 1. Obtenemos la data del repositorio (KPIs, Tabla y ahora Gráfico dinámico)
            var reporte = await _repo.ObtenerReporteAsync(filtros);

            return reporte;
        }
    }
}
