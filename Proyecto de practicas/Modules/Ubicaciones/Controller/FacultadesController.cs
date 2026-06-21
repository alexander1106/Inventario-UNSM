using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultadesController : ControllerBase
    {
        private readonly IFacultadesService _service;

        public FacultadesController(IFacultadesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var facultad = await _service.GetByIdAsync(id);

            if (facultad == null)
                return NotFound("Facultad no encontrada.");

            return Ok(facultad);
        }

        [HttpGet("sede/{sedeId}")]
        public async Task<IActionResult> GetBySede(int sedeId)
        {
            return Ok(await _service.GetBySedeIdAsync(sedeId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Facultades facultad)
        {
            try
            {
                var result = await _service.CreateAsync(facultad);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.Id },
                    result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("detalle")]
        public async Task<IActionResult> GetAllDetalle()
        {
            return Ok(await _service.GetAllDetalleAsync());
        }

        [HttpGet("detalle/{id}")]
        public async Task<IActionResult> GetDetalleById(int id)
        {
            var facultad = await _service.GetDetalleByIdAsync(id);

            if (facultad == null)
                return NotFound("Facultad no encontrada.");

            return Ok(facultad);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Facultades facultad)
        {
            try
            {
                var result = await _service.UpdateAsync(id, facultad);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return Ok(new
                {
                    mensaje = "Facultad eliminada correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}