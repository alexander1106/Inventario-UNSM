using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Modules.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolPermisosController : ControllerBase
    {
        private readonly IRolPermisosService _service;

        public RolPermisosController(IRolPermisosService service)
        {
            _service = service;
        }

        // ✅ GET UNO
        [HttpGet("{rolId}/{subModuloId}/{permisoId}")]
        public async Task<IActionResult> Get(int rolId, int subModuloId, int permisoId)
        {
            var result = await _service.GetAsync(rolId, subModuloId, permisoId);

            if (result == null)
                return NotFound(new ApiResponse<object>(false, "No encontrado", null));

            return Ok(new ApiResponse<object>(true, "OK", result));
        }

        // ✅ GET POR ROL
        [HttpGet("rol/{rolId}")]
        public async Task<IActionResult> GetByRol(int rolId)
        {
            var result = await _service.GetByRolAsync(rolId);

            return Ok(new ApiResponse<object>(true, "OK", result));
        }

        // ✅ CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<RolPermisosDTO> dtos)
        {
            var creados = new List<RolPermisosDTO>();

            foreach (var dto in dtos)
            {
                var item = await _service.CreateAsync(dto);
                creados.Add(item);
            }

            return Ok(new ApiResponse<object>(true, "Creado", creados));
        }

        // ✅ DELETE
        [HttpDelete("{rolId}/{subModuloId}/{permisoId}")]
        public async Task<IActionResult> Delete(int rolId, int subModuloId, int permisoId)
        {
            var result = await _service.DeleteAsync(rolId, subModuloId, permisoId);

            if (!result)
                return NotFound(new ApiResponse<object>(false, "No encontrado", null));

            return Ok(new ApiResponse<object>(true, "Eliminado correctamente", null));
        }

        // 🔥 UPDATE INDIVIDUAL
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolPermisosDTO dto)
        {
            var result = await _service.UpdateByIdAsync(id, dto);

            return Ok(new ApiResponse<object>(true, "Actualizado", result));
        }

        // 🚀 SYNC MASIVO
        [HttpPut("sync/{rolId}")]
        public async Task<IActionResult> Sync(int rolId, [FromBody] List<RolPermisosDTO> permisos)
        {
            await _service.SyncPermisosAsync(rolId, permisos);

            return Ok(new ApiResponse<object>(true, "Sincronizado", null));
        }

        [HttpGet("{rolId}/accesos")]
        public async Task<IActionResult> GetAccesosPorRol(int rolId)
        {
            var result = await _service.GetAccesosPorRolAsync(rolId);

            return Ok(new ApiResponse<object>(true, "OK", result));
        }
    }
}