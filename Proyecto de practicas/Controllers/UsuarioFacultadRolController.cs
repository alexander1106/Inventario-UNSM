using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioFacultadRolController : ControllerBase
    {
        private readonly IUsuarioFacultadRolService _service;

        public UsuarioFacultadRolController(IUsuarioFacultadRolService service)
        {
            _service = service;
        }

        /// <summary>
        /// Asigna un rol a un usuario dentro de una facultad.
        /// </summary>
        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarUsuarioFacultadRol([FromBody] UsuarioFacultadRolDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _service.AsignarUsuarioFacultadRolAsync(dto);

            if (resultado.Contains("ya existe"))
                return Conflict(resultado);

            return Ok(resultado);
        }

        /*
          [HttpGet("listar")]
          public async Task<ActionResult<IEnumerable<UsuarioFacultadRolDTO>>> GetAsignaciones()
          {
              var asignaciones = await _service.ListarAsignacionesAsync();
              return Ok(asignaciones);
          }
        */
    }
}