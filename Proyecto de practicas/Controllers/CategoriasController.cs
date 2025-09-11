using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Service;
using System;
namespace Proyecto_de_practicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasService _service;

        public CategoriasController(ICategoriasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cat = await _service.GetListCategorias();
            return Ok(cat);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cat = await _service.GetCategorias(id);
            if (cat == null) return NotFound();
            return Ok(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Categorias cat)
        {
            try
            {
                var nuevo = await _service.AddCategorias(cat);
                return CreatedAtAction(nameof(Get), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categorias cat)
        {
            try
            {
                cat.Id = id;
                var actualizado = await _service.ActualizarCategoriaAsync(cat);
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
            var eliminado = await _service.EliminarCategoriaAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
