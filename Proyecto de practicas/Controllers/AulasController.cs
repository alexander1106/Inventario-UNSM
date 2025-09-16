using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Service;
using SistemaInventario.DTO;

namespace Proyecto_de_practicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulasController: ControllerBase
    {
    
            private readonly IAulasService _service;

            public AulasController(IAulasService service)
            {
                _service = service;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var labs = await _service.GetListAula();
                return Ok(labs);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var lab = await _service.GetAula(id);
                if (lab == null) return NotFound();
                return Ok(lab);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] AulasDto aulaDto)
            {
            try
            {
                var aula = new Aulas
                {
                    Nombre = aulaDto.Nombre,
                    Estado = aulaDto.Estado,
                    PisosId = aulaDto.PisosId
                };
                var nuevo = await _service.AddAula(aula);
                var nuevoDto = new AulasDto
                {
                    Id = nuevo.Id,
                    Nombre = nuevo.Nombre,
                    Estado = nuevo.Estado,
                    PisosId = nuevo.PisosId
                };
                return CreatedAtAction(nameof(Get), new { id = nuevo.Id }, nuevoDto);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }               
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] Aulas lab)
            {
                try
                {
                    lab.Id = id;
                    var actualizado = await _service.ActuallizarAula(lab);
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
                var eliminado = await _service.EliminarAula(id);
                if (!eliminado) return NotFound();
                return NoContent();
            }
        }
    }
