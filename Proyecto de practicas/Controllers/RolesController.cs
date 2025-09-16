using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var rolesDto = await _rolesService.GetAllRolesAsync();
            return Ok(rolesDto);
        }

        // GET: api/roles/existe/{nombre}
        [HttpGet("existe/{nombre}")]
        public async Task<IActionResult> RoleExists(string nombre)
        {
            var exists = await _rolesService.RoleExistsAsync(nombre);
            return Ok(new { exists });
        }

        // POST: api/roles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolesDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { message = "El nombre del rol no puede estar vacío." });

            try
            {
                var result = await _rolesService.AddRoleAsync(dto);
                return CreatedAtAction(nameof(RoleExists), new { nombre = dto.Nombre }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _rolesService.DeleteRol(id);
            if (!eliminado)
                return NotFound(new { message = $"El rol con id {id} no existe." });
            return Ok(new { message = $"El rol con id {id} fue eliminado exitosamente." });
        }
    }
}
