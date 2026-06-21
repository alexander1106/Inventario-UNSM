using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SedesController : ControllerBase
    {
        private readonly ISedesService _service;

        public SedesController(ISedesService service)
        {
            _service = service;
        }

        // GET NORMAL
        [HttpGet]
        public async Task<IActionResult> GetAllDetalle()
        {
            var sedes = await _service.GetAllDetalleAsync();
            return Ok(sedes);
        }

        // GET NORMAL POR ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sede = await _service.GetByIdAsync(id);

            if (sede == null)
                return NotFound("Sede no encontrada.");

            return Ok(sede);
        }

        // GET DETALLE POR ID
        [HttpGet("detalle/{id}")]
        public async Task<IActionResult> GetDetalleById(int id)
        {
            var sede = await _service.GetDetalleByIdAsync(id);

            if (sede == null)
                return NotFound("Sede no encontrada.");

            return Ok(sede);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Sedes sede)
        {
            try
            {
                var result = await _service.CreateAsync(sede);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Sedes sede)
        {
            try
            {
                var result = await _service.UpdateAsync(id, sede);
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
                    mensaje = "Sede eliminada correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}