using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultadesController : ControllerBase
    {
        private readonly IFacultadesService _service;

        public FacultadesController(IFacultadesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fac = await _service.GetListFacultades();
            return Ok(fac);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var fac = await _service.GetFacultades(id);
            if (fac == null) return NotFound();
            return Ok(fac);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Facultades fac)
        {
            try
            {
                var nuevo = await _service.AddFacultades(fac);
                return CreatedAtAction(nameof(Get), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Facultades fac)
        {
            try
            {
                fac.Id = id;
                var actualizado = await _service.ActualizarFacultadAsync(fac);
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
            var eliminado = await _service.EliminarFacultadAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
