using global::Proyecto_de_practicas.Modules.Mantenimiento.DTO;
using global::Proyecto_de_practicas.Modules.Mantenimiento.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Mantenimiento.DTO;
using Proyecto_de_practicas.Modules.Mantenimiento.Service.IService;

namespace Proyecto_de_practicas.Modules.Mantenimiento.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MantenimientosController : ControllerBase
    {
        private readonly IMantenimientosService _service;

        public MantenimientosController(IMantenimientosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetById(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MantenimientosCreateDTO dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] MantenimientosUpdateDTO dto)
        {
            var result = await _service.UpdateEstado(id, dto);

            if (!result)
                return NotFound();

            return Ok("Estado actualizado correctamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);

            if (!result)
                return NotFound();

            return Ok("Eliminado correctamente");
        }
    }
}