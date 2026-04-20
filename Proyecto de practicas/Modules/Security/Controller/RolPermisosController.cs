using Microsoft.AspNetCore.Mvc;
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

        // ✅ GET UNO (CORRECTO)
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int rolId,
            [FromQuery] int permisoId,
            [FromQuery] int? moduloId,
            [FromQuery] int? subModuloId)
        {
            var result = await _service.GetAsync(rolId, moduloId, subModuloId, permisoId);

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<RolPermisosDTO> dtos)
        {
            var result = new List<RolPermisosDTO>();

            foreach (var dto in dtos)
            {
                result.Add(await _service.CreateAsync(dto));
            }

            return Ok(new ApiResponse<object>(true, "Permisos asignados correctamente", result));
        }

        // ✅ DELETE (CORRECTO)
        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromQuery] int rolId,
            [FromQuery] int permisoId,
            [FromQuery] int? moduloId,
            [FromQuery] int? subModuloId)
        {
            var result = await _service.DeleteAsync(rolId, moduloId, subModuloId, permisoId);

            if (!result)
                return NotFound(new ApiResponse<object>(false, "No encontrado", null));

            return Ok(new ApiResponse<object>(true, "Eliminado correctamente", null));
        }

        // 🔥 UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolPermisosDTO dto)
        {
            var result = await _service.UpdateByIdAsync(id, dto);
            return Ok(new ApiResponse<object>(true, "Actualizado", result));
        }

        // 🚀 SYNC
        [HttpPut("sync/{rolId}")]
        public async Task<IActionResult> Sync(int rolId, [FromBody] List<RolPermisosDTO> permisos)
        {
            await _service.SyncPermisosAsync(rolId, permisos);
            return Ok(new ApiResponse<object>(true, "Sincronizado", null));
        }

        // ✅ ACCESOS
        [HttpGet("{rolId}/accesos")]
        public async Task<IActionResult> GetAccesosPorRol(int rolId)
        {
            var result = await _service.GetAccesosPorRolAsync(rolId);
            return Ok(new ApiResponse<object>(true, "OK", result));
        }
    }
}