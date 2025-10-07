using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.DTOs;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [Route("api/ubicaciones")]
    [ApiController]
    public class UbicacionController : ControllerBase
    {
        private readonly IUbicacionService _service;

        public UbicacionController(IUbicacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<UbicacionDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UbicacionDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UbicacionDto>> Create(UbicacionDto dto)
        {
            try
            {
                var result = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Aquí devolvemos el mensaje de duplicado
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UbicacionDto>> Update(int id, UbicacionDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // También para actualizar duplicados
            }
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
