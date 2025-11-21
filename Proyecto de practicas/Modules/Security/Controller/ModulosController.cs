using Microsoft.AspNetCore.Mvc;
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
            var modulos = await _service.GetAllAsync();
            return Ok(modulos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var modulo = await _service.GetByIdAsync(id);

            if (modulo == null)
                return NotFound(new { message = "El módulo no existe." });

            return Ok(modulo);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> SearchByNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest(new { message = "Debe proporcionar un nombre para buscar." });

            var resultado = await _service.SearchByNombreAsync(nombre);
            return Ok(resultado);
        }
    }
}
