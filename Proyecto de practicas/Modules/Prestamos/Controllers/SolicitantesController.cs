using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Prestamos.DTO;
using Proyecto_de_practicas.Modules.Prestamos.Services.IServices;

namespace Proyecto_de_practicas.Modules.Prestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitantesController : ControllerBase
    {
        private readonly ISolicitanteService _service;

        public SolicitantesController(ISolicitanteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();

            return Ok(new ApiResponse<object>(
                true,
                "Solicitantes obtenidos correctamente",
                data
            ));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    "Solicitante no encontrado",
                    null
                ));
            }

            return Ok(new ApiResponse<object>(
                true,
                "Solicitante obtenido correctamente",
                data
            ));
        }

[HttpPost]
public async Task<IActionResult> Create(SolicitanteDto dto)
{
    try
    {
        var data = await _service.CreateAsync(dto);

        return Ok(new ApiResponse<object>(
            true,
            "Solicitante registrado correctamente",
            data
        ));
    }
    catch (Exception ex)
    {
        return BadRequest(new ApiResponse<object>(
            false,
            ex.Message,
            null
        ));
    }
}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SolicitanteDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    "Solicitante no encontrado",
                    null
                ));
            }

            return Ok(new ApiResponse<object>(
                true,
                "Solicitante actualizado correctamente",
                null
            ));
        }

        [HttpGet("por-usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var data = await _service.GetByUsuarioAsync(usuarioId);

            return Ok(new ApiResponse<object>(
                true,
                "Solicitantes filtrados por usuario",
                data
            ));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    "Solicitante no encontrado",
                    null
                ));
            }

            return Ok(new ApiResponse<object>(
                true,
                "Solicitante eliminado correctamente",
                null
            ));
        }
    }
}