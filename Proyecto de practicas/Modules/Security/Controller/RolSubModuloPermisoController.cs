using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Services.IServices;


namespace Proyecto_de_practicas.Modules.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolSubModuloPermisoController : ControllerBase
    {
        private readonly IRolSubModuloPermisoService _service;

        public RolSubModuloPermisoController(IRolSubModuloPermisoService service)
        {
            _service = service;
        }

        // GET: api/RolSubModuloPermiso/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        // GET: api/RolSubModuloPermiso/rolSubModulo/{rolSubModuloId}
        [HttpGet("rolSubModulo/{rolSubModuloId}")]
        public async Task<IActionResult> GetByRolSubModulo(int rolSubModuloId)
        {
            var list = await _service.GetByRolSubModuloAsync(rolSubModuloId);
            return Ok(list);
        }

        // POST: api/RolSubModuloPermiso
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolSubModuloPermisoDto dto)
        {
            var created = await _service.CreateAsync(dto.RolSubModuloId, dto.PermisoId);
            return Ok(created);
        }

        // PUT: api/RolSubModuloPermiso/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolSubModuloPermisoDto dto)
        {
            dto.Id = id;
            var updated = await _service.UpdateAsync(dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        // DELETE: api/RolSubModuloPermiso/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 
