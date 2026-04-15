using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Controllers.Security
{
    [Route("api/sub-modulos")]
    [ApiController]
    public class SubModulosController : ControllerBase
    {
        private readonly ISubModulosService _service;

        public SubModulosController(ISubModulosService service)
        {
            _service = service;
        }

        // GET: api/sub-modulos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var submodulos = await _service.GetAllAsync();

                return Ok(new ApiResponse<object>(
                    true,
                    "Lista de submódulos obtenida correctamente",
                    submodulos
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

        // GET: api/sub-modulos/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var submodulo = await _service.GetByIdAsync(id);

                if (submodulo == null)
                {
                    return NotFound(new ApiResponse<object>(
                        false,
                        "El submódulo no existe",
                        null
                    ));
                }

                return Ok(new ApiResponse<object>(
                    true,
                    "Submódulo encontrado",
                    submodulo
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

        // GET: api/sub-modulos/modulo/{moduloId}
        [HttpGet("modulo/{moduloId:int}")]
        public async Task<IActionResult> GetByModulo(int moduloId)
        {
            try
            {
                var submodulos = await _service.GetByModuloIdAsync(moduloId);

                return Ok(new ApiResponse<object>(
                    true,
                    "Submódulos del módulo obtenidos correctamente",
                    submodulos
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

        // GET: api/sub-modulos/buscar?nombre=xx
        [HttpGet("buscar")]
        public async Task<IActionResult> SearchByNombre([FromQuery] string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return BadRequest(new ApiResponse<object>(
                        false,
                        "Debe proporcionar un nombre para buscar",
                        null
                    ));
                }

                var resultado = await _service.SearchByNombreAsync(nombre);

                return Ok(new ApiResponse<object>(
                    true,
                    "Resultado de búsqueda",
                    resultado
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
    }
}