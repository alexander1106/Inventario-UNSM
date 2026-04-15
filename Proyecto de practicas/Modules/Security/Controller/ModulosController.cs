using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Controllers.Security
{
    [Route("api/modulos")]
    [ApiController]
    public class ModulosController : ControllerBase
    {
        private readonly IModulosService _service;

        public ModulosController(IModulosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var modulos = await _service.GetAllAsync();

                return Ok(new ApiResponse<object>(
                    true,
                    "Lista de módulos obtenida correctamente",
                    modulos
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var modulo = await _service.GetByIdAsync(id);

                if (modulo == null)
                {
                    return NotFound(new ApiResponse<object>(
                        false,
                        "El módulo no existe",
                        null
                    ));
                }

                return Ok(new ApiResponse<object>(
                    true,
                    "Módulo encontrado",
                    modulo
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