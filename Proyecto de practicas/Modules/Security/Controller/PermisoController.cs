using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Modules.Security.Controller
{
    [Route("api/permisos")]
    [ApiController]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoService _service;

        public PermisoController(IPermisoService service)
        {
            _service = service;
        }

        // GET: api/permisos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _service.GetAllAsync();

                return Ok(new ApiResponse<object>(
                    true,
                    "Lista de permisos obtenida correctamente",
                    list
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(
                    false,
                    "Error interno del servidor",
                    null,
                    ex.Message
                ));
            }
        }

        // GET: api/permisos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var permiso = await _service.GetAsync(id);

                if (permiso == null)
                {
                    return NotFound(new ApiResponse<object>(
                        false,
                        "Permiso no encontrado",
                        null
                    ));
                }

                return Ok(new ApiResponse<object>(
                    true,
                    "Permiso encontrado",
                    permiso
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(
                    false,
                    "Error interno del servidor",
                    null,
                    ex.Message
                ));
            }
        }

        // POST: api/permisos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermisoDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);

                return Ok(new ApiResponse<object>(
                    true,
                    "Permiso creado correctamente",
                    created
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(
                    false,
                    "Error al crear el permiso",
                    null,
                    ex.Message
                ));
            }
        }

        // PUT: api/permisos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermisoDto dto)
        {
            try
            {
                dto.Id = id;
                var updated = await _service.UpdateAsync(dto);

                if (updated == null)
                {
                    return NotFound(new ApiResponse<object>(
                        false,
                        "Permiso no encontrado para actualizar",
                        null
                    ));
                }

                return Ok(new ApiResponse<object>(
                    true,
                    "Permiso actualizado correctamente",
                    updated
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(
                    false,
                    "Error al actualizar el permiso",
                    null,
                    ex.Message
                ));
            }
        }

        // DELETE: api/permisos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return Ok(new ApiResponse<object>(
                    true,
                    "Permiso eliminado correctamente",
                    null
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(
                    false,
                    "Error al eliminar el permiso",
                    null,
                    ex.Message
                ));
            }
        }
    }
}