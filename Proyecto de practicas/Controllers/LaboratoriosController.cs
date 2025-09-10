using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Service;
using System;

namespace Proyecto_de_practicas.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoriosController : ControllerBase
    {
        private readonly ILaboratoriosService _service;

        public LaboratoriosController(ILaboratoriosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var labs = await _service.GetListLaboratorios();
            return Ok(labs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var lab = await _service.GetLaboratorios(id);
            if (lab == null) return NotFound();
            return Ok(lab);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Laboratorios lab)
        {
            try
            {
                var nuevo = await _service.AddLaboratorios(lab);
                return CreatedAtAction(nameof(Get), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Laboratorios lab)
        {
            try
            {
                lab.Id = id;
                var actualizado = await _service.ActualizarLaboratorioAsync(lab);
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
            var eliminado = await _service.EliminarLaboratorioAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}

