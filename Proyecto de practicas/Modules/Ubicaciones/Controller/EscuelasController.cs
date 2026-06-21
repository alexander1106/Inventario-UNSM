using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EscuelasController : ControllerBase
    {
        private readonly IEscuelasService _service;

        public EscuelasController(IEscuelasService service)
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
            var escuela = await _service.GetByIdAsync(id);

            if (escuela == null)
                return NotFound("Escuela no encontrada.");

            return Ok(escuela);
        }

        [HttpGet("facultad/{facultadId}")]
        public async Task<IActionResult> GetByFacultad(int facultadId)
        {
            return Ok(await _service.GetByFacultadIdAsync(facultadId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Escuelas escuela)
        {
            try
            {
                var result = await _service.CreateAsync(escuela);

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
        public async Task<IActionResult> Update(int id, [FromBody] Escuelas escuela)
        {
            try
            {
                var result = await _service.UpdateAsync(id, escuela);
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
                    mensaje = "Escuela eliminada correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}