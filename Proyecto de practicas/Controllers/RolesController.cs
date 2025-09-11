using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var roles = await _rolesService.GetAllRolesAsync();
            return Ok(roles);
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
        [Authorize]
        public async Task<IActionResult> Create([FromBody] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest("El nombre del rol no puede estar vacío.");

            await _rolesService.AddRoleAsync(nombre);
            return CreatedAtAction(nameof(RoleExists), new { nombre = nombre }, new { nombre });
        }

        // DELETE: api/roles/{nombre}
        [HttpDelete("{nombre}")]
        [Authorize]
        public async Task<IActionResult> Delete(string nombre)
        {
            var exists = await _rolesService.RoleExistsAsync(nombre);
            if (!exists) return NotFound($"El rol '{nombre}' no existe.");

            await _rolesService.DeleteRoleAsync(nombre);
            return NoContent();
        }
    }
}