using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Service;
using System;

namespace Proyecto_de_practicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquiposController : ControllerBase
    {
        private readonly IEquiposService _equiposService;

        public EquiposController(IEquiposService equiposService)
        {
            _equiposService = equiposService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var equipo = await _equiposService.GetListEquipos();
            return Ok(equipo);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var equipo = await _equiposService.GetEquipos(id);
            if (equipo == null) return NotFound();
            return Ok(equipo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Equipos equipo)
        {
            try
            {
                var nuevo = await _equiposService.AddEquipos(equipo);
                return CreatedAtAction(nameof(Get), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Equipos equipo)
        {
            try
            {
                equipo.Id = id;
                var actualizado = await _equiposService.ActualizarEquipoAsync(equipo);
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
            var eliminado = await _equiposService.EliminarEquipoAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }


}

