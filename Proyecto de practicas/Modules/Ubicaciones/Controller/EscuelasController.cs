using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
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
        public async Task<IActionResult> Create([FromForm] Escuelas escuela, IFormFile? imagen)
        {
            try
            {
                var result = await _service.CreateAsync(escuela, imagen);

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
        public async Task<IActionResult> Update(int id, [FromForm] Escuelas escuela, IFormFile? imagen)
        {
            try
            {
                var result = await _service.UpdateAsync(id, escuela, imagen);
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

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var escuela = await _service.GetByUsuarioIdAsync(usuarioId);
            if (escuela == null)
                return NotFound(new { success = false, mensaje = "No hay escuela asignada a este usuario." });

            return Ok(new { success = true, data = escuela });
        }

        [HttpPut("{id}/asignar-usuario")]
        public async Task<IActionResult> AsignarUsuario(int id, [FromBody] AsignarUsuarioUbicacionDto dto)
        {
            try
            {
                var result = await _service.AsignarUsuarioAsync(id, dto.UsuarioId);
                return Ok(new { mensaje = "Usuario asignado correctamente a la escuela.", escuela = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}