using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PisosController : ControllerBase
    {
        private readonly IPisosService _service;

        public PisosController(IPisosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var piso = await _service.GetListPisos();
            return Ok(piso);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var piso = await _service.GetPisos(id);
            if (piso == null) return NotFound();
            return Ok(piso);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pisos piso)
        {
            try
            {
                var nuevo = await _service.AddPisos(piso);
                return CreatedAtAction(nameof(Get), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pisos piso)
        {
            try
            {
                piso.Id = id;
                var actualizado = await _service.ActualizarPisoAsync(piso);
                if (actualizado == null) return NotFound();
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.EliminarPisoAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
