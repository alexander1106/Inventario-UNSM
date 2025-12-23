using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Security.Services.IServices;
using Proyecto_de_practicas.Modules.Security.DTO;

namespace Proyecto_de_practicas.Modules.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolSubModuloController : ControllerBase
    {
        private readonly IRolSubModuloService _service;

        public RolSubModuloController(IRolSubModuloService service)
        {
            _service = service;
        }

        [HttpGet("{rolId}/{subModuloId}")]
        public async Task<IActionResult> Get(int rolId, int subModuloId)
        {
            var result = await _service.GetAsync(rolId, subModuloId);
            return result == null ? NotFound() : Ok(result);
        }
        // Por esto
        [HttpGet("rol/{rolId}")]
        public async Task<IActionResult> GetByRol(int rolId)
        {
            var list = await _service.GetModulosConSubModulosPorRolAsync(rolId); // <-- devuelve agrupado
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int rolId, int subModuloId)
        {
            var created = await _service.CreateAsync(rolId, subModuloId);
            return Ok(created);
        }

        [HttpDelete("{rolId}/{subModuloId}")]
        public async Task<IActionResult> Delete(int rolId, int subModuloId)
        {
            await _service.DeleteAsync(rolId, subModuloId);
            return NoContent();
        }

        // PUT api/RolSubModulo/actualizar-submodulos/5
        // Body: [1, 3, 7]  ← lista de Ids de submódulos
        [HttpPut("actualizar-submodulos/{rolId}")]
        public async Task<IActionResult> ActualizarSubModulos(int rolId, [FromBody] List<int> subModulosIds)

        {
            await _service.ActualizarSubModulosAsync(rolId, subModulosIds);
            return Ok(new { message = "Submódulos actualizados correctamente." });
        }
        [HttpGet("rol-modulos/{rolId}")]
        public async Task<IActionResult> GetModulosConSubModulosPorRol(int rolId)
        {
            var result = await _service.GetModulosConSubModulosPorRolAsync(rolId);
            return Ok(result);
        }


    }
}