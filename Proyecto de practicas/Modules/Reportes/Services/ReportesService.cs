using Proyecto_de_practicas.Modules.Reportes.DTO;
using Proyecto_de_practicas.Modules.Reportes.Repository.IReporteRepository;
using Proyecto_de_practicas.Modules.Reportes.Services.IReporteService;

namespace Proyecto_de_practicas.Modules.Reportes.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IReportesRepository _repo;

        public ReportesService(IReportesRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ArticulosPorUbicacionDto>> GetArticulosPorUbicacionAsync()
        {
            return await _repo.GetArticulosPorUbicacionAsync();
        }

        public async Task<List<ArticulosPorTipoDto>> GetArticulosPorTipoAsync()
        {
            return await _repo.GetArticulosPorTipoAsync();
        }
    }
}
