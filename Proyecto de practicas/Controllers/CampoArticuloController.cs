using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [Route("api/campos-articulo")]
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

        

        [HttpGet("tipo-articulo/{tipoArticuloId}")]
        public async Task<ActionResult<List<CampoArticuloDto>>> GetByTipoArticulo(int tipoArticuloId)
        {
            var result = await _service.GetByTipoArticuloIdAsync(tipoArticuloId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<CampoArticuloDto>> Create(CampoArticuloDto dto)
        {
            try
            {
                var result = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<CampoArticuloDto>> Update(int id, CampoArticuloDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
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
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("lote")]
        public async Task<ActionResult> CreateMultiple(List<CampoArticuloDto> campos)
        {
            foreach (var campo in campos)
            {
                await _service.AddAsync(campo);
            }
            return Ok();
        }

    }
}
