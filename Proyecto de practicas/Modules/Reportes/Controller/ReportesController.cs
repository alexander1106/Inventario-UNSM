    using Microsoft.AspNetCore.Mvc;
    using Proyecto_de_practicas.Modules.Reportes.DTO;
    using Proyecto_de_practicas.Modules.Reportes.Services.IReporteService;
    namespace Proyecto_de_practicas.Modules.Reportes.Controller
    {
        [Route("api/reportes")]
        [ApiController]
        public class ReportesController : ControllerBase
        {
            private readonly IReportesService _service;

            public ReportesController(IReportesService service)
            {
                _service = service;
            }

            // 🔹 Endpoint para artículos por ubicación
            [HttpGet("articulos-por-ubicacion")]
            public async Task<ActionResult<List<ArticulosPorUbicacionDto>>> GetArticulosPorUbicacion()
            {
                var result = await _service.GetArticulosPorUbicacionAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        message = "No se encontraron artículos por ubicación.",
                        status = 404
                    });
                }

                return Ok(result);
            }

            // 🔹 Endpoint para artículos por tipo
            [HttpGet("articulos-por-tipo")]
            public async Task<ActionResult<List<ArticulosPorTipoDto>>> GetArticulosPorTipo()
            {
                var result = await _service.GetArticulosPorTipoAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        message = "No se encontraron artículos por tipo.",
                        status = 404
                    });
                }

                return Ok(result);
            }
        }
    }