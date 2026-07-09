using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Controller
{
    [Route("api/ubicaciones")]
    [ApiController]
    public class UbicacionController : ControllerBase
    {
        private readonly IUbicacionService _service;

        public UbicacionController(IUbicacionService service)
        {
            _service = service;
        }
        [HttpGet("por-padre/{padreId}")]
        public async Task<ActionResult<ApiResponse<List<UbicacionDto>>>> GetByPadre(int padreId)
        {
            var result = await _service.GetByPadreAsync(padreId);

            return Ok(new ApiResponse<List<UbicacionDto>>(
                true,
                result.Any() ? "OK" : "Sin datos",
                result
            ));
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UbicacionDto>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<UbicacionDto>>(true, "Lista obtenida", result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UbicacionDto>>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(new ApiResponse<UbicacionDto>(false, "No encontrado", null));

            return Ok(new ApiResponse<UbicacionDto>(true, "OK", result));
        }

        [HttpGet("por-tipo/{tipoId}")]
        public async Task<ActionResult<ApiResponse<List<UbicacionDto>>>> GetByTipo(int tipoId)
        {
            var result = await _service.GetByTipoAsync(tipoId);

            return Ok(new ApiResponse<List<UbicacionDto>>(
                true,
                result.Any() ? "OK" : "Sin datos",
                result
            ));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UbicacionDto>>> Create(
            [FromBody] UbicacionDto dto)
        {
            try
            {
                var result = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = result.Id },
                    new ApiResponse<UbicacionDto>(true, "Creado correctamente", result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<UbicacionDto>(false, ex.Message, null));
            }
        }
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var result = await _service.GetByUsuarioAsync(usuarioId);
            return Ok(result);
        }

        [HttpGet("por-escuela/{escuelaId}")]
        public async Task<ActionResult<ApiResponse<List<UbicacionDto>>>> GetByEscuela(int escuelaId)
        {
            var result = await _service.GetByEscuelaIdAsync(escuelaId);
            return Ok(new ApiResponse<List<UbicacionDto>>(
                true,
                result.Any() ? "OK" : "Sin datos",
                result
            ));
        }

        [HttpPut("{id}/asignar-usuario")]
        public async Task<IActionResult> AsignarUsuario(int id, [FromBody] AsignarUsuarioUbicacionDto dto)
        {
            try
            {
                var ubicacion = await _service.AsignarUsuarioAsync(id, dto.UsuarioId);

                return Ok(new ApiResponse<UbicacionDto>(
                    true,
                    "Usuario asignado correctamente",
                    ubicacion
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(false, ex.Message, null));
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UbicacionDto>>> Update(
            int id,
            [FromBody] UbicacionDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);

                return Ok(new ApiResponse<UbicacionDto>(true, "Actualizado", result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<UbicacionDto>(false, ex.Message, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);

                if (!success)
                    return NotFound(new ApiResponse<object>(false, "No encontrado", null));

                return Ok(new ApiResponse<object>(true, "Eliminado", null));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(false, ex.Message, null));
            }
        }
    }
}