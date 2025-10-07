using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampoArticuloController : ControllerBase
    {
        private readonly ICampoArticuloService _service;

        public CampoArticuloController(ICampoArticuloService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CampoArticuloDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampoArticuloDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("tipo/{tipoArticuloId}")]
        public async Task<ActionResult<List<CampoArticuloDto>>> GetByTipoArticulo(int tipoArticuloId)
        {
            var result = await _service.GetByTipoArticuloIdAsync(tipoArticuloId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CampoArticuloDto>> Create(CampoArticuloDto dto)
        {
            var result = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CampoArticuloDto>> Update(int id, CampoArticuloDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
