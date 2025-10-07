using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [Route("api/tipo-ubicacion")]
    [ApiController]
    public class TipoUbicacionController : ControllerBase
    {
        private readonly ITipoUbicacionService _service;

        public TipoUbicacionController(ITipoUbicacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoUbicacion>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUbicacion>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TipoUbicacion>> Create([FromBody] TipoUbicacion tipoUbicacion)
        {
            try
            {
                var result = await _service.AddAsync(tipoUbicacion);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Captura duplicado de nombre
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoUbicacion>> Update(int id, [FromBody] TipoUbicacion tipoUbicacion)
        {
            try
            {
                var result = await _service.UpdateAsync(id, tipoUbicacion);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Captura duplicado de nombre al actualizar
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Captura si hay ubicaciones asociadas
            }
        }
    }
}
