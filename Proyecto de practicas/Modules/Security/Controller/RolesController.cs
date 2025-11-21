using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Modules.Security.Controller
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _rolesService.GetAllRolesAsync();
            return Ok(roles);
        }

        // GET: api/roles/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rol = await _rolesService.GetByIdAsync(id);
            if (rol == null)
                return NotFound(new { message = "Rol no encontrado." });

            return Ok(rol);
        }

        // GET: api/roles/nombre/{nombre}
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> GetByNombre(string nombre)
        {
            var rol = await _rolesService.GetByNombreAsync(nombre);
            if (rol == null)
                return NotFound(new { message = "Rol no encontrado." });

            return Ok(rol);
        }

        // POST: api/roles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolesDTO dto)
        {
            try
            {
                var creado = await _rolesService.AddRoleAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/roles/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolesDTO dto)
        {
            if (dto.Id != id)
                return BadRequest(new { message = "El ID del cuerpo no coincide con la URL." });

            try
            {
                var actualizado = await _rolesService.UpdateRoleAsync(dto);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/roles/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _rolesService.DeleteRol(id);

            if (!eliminado)
                return NotFound(new { message = "Rol no encontrado." });

            return Ok(new { message = "Rol eliminado correctamente." });
        }
    }
}
