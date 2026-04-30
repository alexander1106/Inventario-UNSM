using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Reporte.DTO;
using Proyecto_de_practicas.Modules.Reporte.Service.IService;

namespace Proyecto_de_practicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesService _service;

        public ReportesController(IReportesService service)
        {
            _service = service;
        }

        [HttpPost("generar")]
        public async Task<IActionResult> GenerarReporte([FromBody] ReporteRequestDto filtros)
        {
            try
            {
                var resultado = await _service.GenerarReporteAsync(filtros);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al generar el reporte", details = ex.Message });
            }
        }
    }
}