using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Modules.Security.Controller
{
    [Route("api/permisos")]
    [ApiController]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoService _service;

        public PermisoController(IPermisoService service)
        {
            _service = service;
        }

        // GET: api/Permiso
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        // GET: api/Permiso/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var permiso = await _service.GetAsync(id);
            return permiso == null ? NotFound() : Ok(permiso);
        }

        // POST: api/Permiso
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermisoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }

        // PUT: api/Permiso/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermisoDto dto)
        {
            dto.Id = id;
            var updated = await _service.UpdateAsync(dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        // DELETE: api/Permiso/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}