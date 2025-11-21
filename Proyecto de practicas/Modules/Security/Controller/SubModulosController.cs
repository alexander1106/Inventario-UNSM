using Microsoft.AspNetCore.Mvc;
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

        // GET: api/submodulos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var submodulos = await _service.GetAllAsync();
            return Ok(submodulos);
        }

        // GET: api/submodulos/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var submodulo = await _service.GetByIdAsync(id);

            if (submodulo == null)
                return NotFound(new { message = "El submódulo no existe." });

            return Ok(submodulo);
        }

        // GET: api/submodulos/modulo/3
        [HttpGet("modulo/{moduloId:int}")]
        public async Task<IActionResult> GetByModulo(int moduloId)
        {
            var submodulos = await _service.GetByModuloIdAsync(moduloId);
            return Ok(submodulos);
        }

        // GET: api/submodulos/buscar?nombre=xx
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
