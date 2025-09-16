using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.DTO;
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
            var pisos = await _service.GetListPisos();
            return Ok(pisos);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var piso = await _service.GetPisos(id);
            if (piso == null) return NotFound();

            var pisoDto = new PisosDto
            {
                Id = piso.Id,
                Numero = piso.Numero
            };

            return Ok(pisoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PisosDto pisoDto)
        {
            try
            {
                var piso = new Pisos
                {
                    Numero = pisoDto.Numero,
                    FacultadId = pisoDto.FacultadId
                };
                var nuevo = await _service.AddPisos(piso);

                // Mapear de nuevo a DTO para la respuesta
                var nuevoDto = new PisosDto
                {
                    Id = nuevo.Id,
                    Numero = nuevo.Numero,
                    FacultadId = nuevo.FacultadId
                };

                return CreatedAtAction(nameof(Get), new { id = nuevo.Id }, nuevoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PisosDto pisoDto)
        {
            try
            {
                var piso = new Pisos
                {
                    Id = id,
                    Numero = pisoDto.Numero,
                    FacultadId = pisoDto.FacultadId
                };

                var actualizado = await _service.ActualizarPisoAsync(piso);
                if (actualizado == null) return NotFound();

                var actualizadoDto = new PisosDto
                {
                    Id = actualizado.Id,
                    Numero = actualizado.Numero,
                    FacultadId = actualizado.FacultadId
                };

                return Ok(actualizadoDto);
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
