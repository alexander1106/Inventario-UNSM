using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [Route("api/articulos")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _service;
                    private readonly IArticuloCampoValorService _serviceCamposValor;

        public ArticuloController(IArticuloService service, IArticuloCampoValorService _serviceCampos)
        {
            _service = service;
            _serviceCamposValor = _serviceCampos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticuloDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticuloDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("tipo/{tipoArticuloId}")]
        public async Task<ActionResult<List<ArticuloDto>>> GetByTipoArticulo(int tipoArticuloId)
        {
            var result = await _service.GetByTipoArticuloIdAsync(tipoArticuloId);
            return Ok(result);
        }

        [HttpGet("ubicacion/{ubicacionId}")]
        public async Task<ActionResult<List<ArticuloDto>>> GetByUbicacion(int ubicacionId)
        {
            var result = await _service.GetByUbicacionIdAsync(ubicacionId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ArticuloDto>> Create([FromBody] ArticuloDto dto)
        {
            // 1. Guardar el artículo
            var articulo = await _service.AddAsync(dto);



            return CreatedAtAction(nameof(GetById), new { id = articulo.Id }, articulo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArticuloDto>> Update(int id, ArticuloDto dto)
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