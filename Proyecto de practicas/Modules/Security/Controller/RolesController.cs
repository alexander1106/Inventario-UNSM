using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
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

            return Ok(new ApiResponse<List<RolesDTO>>(
                true,
                "Roles obtenidos correctamente",
                roles
            ));
        }

        // GET: api/roles/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rol = await _rolesService.GetByIdAsync(id);

            if (rol == null)
                return NotFound(new ApiResponse<object>(
                    false,
                    "Rol no encontrado",
                    null
                ));

            return Ok(new ApiResponse<RolesDTO>(
                true,
                "Rol obtenido correctamente",
                rol
            ));
        }

        // GET: api/roles/nombre/{nombre}
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> GetByNombre(string nombre)
        {
            var rol = await _rolesService.GetByNombreAsync(nombre);

            if (rol == null)
                return NotFound(new ApiResponse<object>(
                    false,
                    "Rol no encontrado",
                    null
                ));

            return Ok(new ApiResponse<RolesDTO>(
                true,
                "Rol obtenido correctamente",
                rol
            ));
        }

        // POST: api/roles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolesDTO dto)
        {
            try
            {
                var creado = await _rolesService.AddRoleAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = creado.Id },
                    new ApiResponse<RolesDTO>(
                        true,
                        "Rol creado correctamente",
                        creado
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    ex.Message,
                    null
                ));
            }
        }

        // PUT: api/roles/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolesDTO dto)
        {
            if (dto.Id != id)
                return BadRequest(new ApiResponse<object>(
                    false,
                    "El ID no coincide",
                    null
                ));

            try
            {
                var actualizado = await _rolesService.UpdateRoleAsync(dto);

                return Ok(new ApiResponse<RolesDTO>(
                    true,
                    "Rol actualizado correctamente",
                    actualizado
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    ex.Message,
                    null
                ));
            }
        }

        // DELETE: api/roles/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _rolesService.DeleteRol(id);

            if (!eliminado)
                return NotFound(new ApiResponse<object>(
                    false,
                    "Rol no encontrado",
                    null
                ));

            return Ok(new ApiResponse<object>(
                true,
                "Rol eliminado correctamente",
                null
            ));
        }
    }
}