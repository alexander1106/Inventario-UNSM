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

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UbicacionDto>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<UbicacionDto>>(
                true,
                "Lista de ubicaciones obtenida correctamente",
                result
            ));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UbicacionDto>>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new ApiResponse<UbicacionDto>(
                    false,
                    $"No se encontró ninguna ubicación con el ID {id}",
                    null
                ));
            }
            return Ok(new ApiResponse<UbicacionDto>(
                true,
                "Ubicación encontrada",
                result
            ));
        }

        [HttpGet("por-tipo/{tipoId}")]
        public async Task<ActionResult<ApiResponse<List<UbicacionDto>>>> GetByTipo(int tipoId)
        {
            var result = await _service.GetByTipoAsync(tipoId);
            if (result == null || !result.Any())
            {
                return NotFound(new ApiResponse<List<UbicacionDto>>(
                    false,
                    $"No se encontraron ubicaciones para este tipo de ubicacion",
                    null
                ));
            }
            return Ok(new ApiResponse<List<UbicacionDto>>(
                true,
                "Ubicaciones encontradas",
                result
            ));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UbicacionDto>>> Create([FromBody] UbicacionDto dto)
        {
            try
            {
                var result = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id },
                    new ApiResponse<UbicacionDto>(
                        true,
                        "Ubicación creada correctamente",
                        result
                    )
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<UbicacionDto>(
                    false,
                    ex.Message,
                    null,
                    ex.Message
                ));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UbicacionDto>>> Update(int id, [FromBody] UbicacionDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return Ok(new ApiResponse<UbicacionDto>(
                    true,
                    "Ubicación actualizada correctamente",
                    result
                ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<UbicacionDto>(
                    false,
                    ex.Message,
                    null,
                    ex.Message
                ));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    $"No se encontró la ubicación con ID {id}",
                    null
                ));
            }

            return Ok(new ApiResponse<object>(
                true,
                "Ubicación eliminada correctamente",
                null
            ));
        }
    }
}