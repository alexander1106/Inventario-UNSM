using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
 [ApiController]
    [Route("api/[controller]")]
    public class ArticuloCampoValorController : ControllerBase
    {
        private readonly IArticuloCampoValorService _service;

        public ArticuloCampoValorController(IArticuloCampoValorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("tipo-articulos/{tipoArticuloId}")]
        public async Task<IActionResult> GetByTipoArticuloId(int tipoArticuloId)
        {
            var result = await _service.GetByTipoArticuloIdAsync(tipoArticuloId);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArticuloCampoValorDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Registro creado con éxito");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArticuloCampoValorDto dto)
        {
            if (id != dto.Id) return BadRequest("El id no coincide");
            await _service.UpdateAsync(dto);
            return Ok("Registro actualizado con éxito");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Registro eliminado con éxito");
        }
    }
}
