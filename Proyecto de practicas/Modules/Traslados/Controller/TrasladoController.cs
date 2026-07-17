using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Traslados.DTO;
using Proyecto_de_practicas.Modules.Traslados.Entities;
using Proyecto_de_practicas.Modules.Traslados.Service.IService;

namespace Proyecto_de_practicas.Modules.Traslados.Controller
{

    [Route("api/traslados")]
    [ApiController]
    public class TrasladoController : ControllerBase
    {
        private readonly ITrasladoService _service;

        public TrasladoController(ITrasladoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Traslado>>> GetAll()
        {
            var traslados = await _service.GetAllAsync();
            return Ok(traslados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Traslado>> GetById(int id)
        {
            var traslado = await _service.GetByIdAsync(id);
            if (traslado == null) return NotFound();
            return Ok(traslado);
        }

        [HttpPost]
        public async Task<ActionResult<Traslado>> Create([FromBody] TrasladoDTO dto)
        {
            var traslado = new Traslado
            {
                ArticuloId = dto.ArticuloId,
                UbicacionOrigenId = dto.UbicacionOrigenId,
                UbicacionDestinoId = dto.UbicacionDestinoId,
                FechaTraslado = dto.FechaTraslado,
                UsuarioId = dto.UsuarioId,
                Observaciones = dto.Observaciones,
                
            };

            var created = await _service.CreateAsync(traslado);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Traslado>> Update(int id, [FromBody] TrasladoDTO dto)
        {
            var traslado = await _service.GetByIdAsync(id);
            if (traslado == null) return NotFound();

            traslado.ArticuloId = dto.ArticuloId;
            traslado.UbicacionOrigenId = dto.UbicacionOrigenId;
            traslado.UbicacionDestinoId = dto.UbicacionDestinoId;
            traslado.FechaTraslado = dto.FechaTraslado;
            traslado.UsuarioId = dto.UsuarioId;
            traslado.Observaciones = dto.Observaciones;

            var updated = await _service.UpdateAsync(traslado);
            return Ok(updated);
        }

        [HttpPost("realizar")]
        public async Task<ActionResult> RealizarTraslado([FromBody] TrasladoDTO dto)
        {
            if (dto.UbicacionOrigenId == dto.UbicacionDestinoId)
                return BadRequest("La ubicación origen y destino no pueden ser iguales");

            var traslado = new Traslado
            {
                ArticuloId = dto.ArticuloId,
                UbicacionOrigenId = dto.UbicacionOrigenId,
                UbicacionDestinoId = dto.UbicacionDestinoId,
                FechaTraslado = dto.FechaTraslado,
                UsuarioId = dto.UsuarioId,
                Observaciones = dto.Observaciones
            };

            var ok = await _service.RealizarTrasladoAsync(traslado);

            if (!ok)
                return BadRequest("No se pudo realizar el traslado");

            return Ok(new { mensaje = "Traslado realizado correctamente" });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPost("upload-pdf")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPdf([FromForm] UploadPdfTrasladoDto request)
        {
            try
            {
                var ruta = await _service.UploadPdfAsync(
                    request.TrasladoId,
                    request.File
                );

                return Ok(new ApiResponse<object>(
                    true,
                    "PDF guardado correctamente",
                    new { ruta }
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Error al subir PDF",
                    null,
                    ex.Message
                ));
            }
        }

        [Authorize]
        [HttpPut("{id}/firmar")]
        public async Task<IActionResult> Firmar(int id, [FromQuery] string firmante)
        {
            if (string.IsNullOrWhiteSpace(firmante))
                return BadRequest(new ApiResponse<object>(false, "El nombre del firmante es requerido", null));

            try
            {
                var resultado = await _service.FirmarTrasladoAsync(id, firmante);

                if (resultado == null)
                    return NotFound(new ApiResponse<object>(false, "Traslado no encontrado", null));

                return Ok(new ApiResponse<object>(true, $"Traslado firmado correctamente por {firmante}", resultado));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(false, "Error al firmar el traslado", null, ex.Message));
            }
        }
    }
}
