using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Services;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Modules.Articulos.Controller
{
    [Route("api/clasificacion-depreciacion")]
    [ApiController]
    public class ClasificacionDepreciacionController : ControllerBase
    {
        private readonly IClasificacionDepreciacionService _service;

        public ClasificacionDepreciacionController(IClasificacionDepreciacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ClasificacionDepreciacionDto>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<ClasificacionDepreciacionDto>>(
                true,
                "Lista obtenida correctamente",
                result
            ));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ClasificacionDepreciacionDto>>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new ApiResponse<ClasificacionDepreciacionDto>(
                    false,
                    "No se encontró la clasificación de depreciación",
                    null
                ));
            }

            return Ok(new ApiResponse<ClasificacionDepreciacionDto>(
                true,
                "Clasificación de depreciación encontrada",
                result
            ));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ClasificacionDepreciacionDto>>> Create([FromBody] ClasificacionDepreciacionDto dto)
        {
            try
            {
                var result = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id },
                    new ApiResponse<ClasificacionDepreciacionDto>(
                        true,
                        "Clasificación de depreciación creada correctamente",
                        result
                    )
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<ClasificacionDepreciacionDto>(
                    false,
                    ex.Message,
                    null,
                    ex.Message
                ));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ClasificacionDepreciacionDto>>> Update(int id, [FromBody] ClasificacionDepreciacionDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return Ok(new ApiResponse<ClasificacionDepreciacionDto>(
                    true,
                    "Clasificación de depreciación actualizada correctamente",
                    result
                ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<ClasificacionDepreciacionDto>(
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
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                {
                    return NotFound(new ApiResponse<object>(
                        false,
                        "No se encontró la clasificación de depreciación",
                        null
                    ));
                }

                return Ok(new ApiResponse<object>(
                    true,
                    "Clasificación de depreciación eliminada exitosamente",
                    null
                ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    ex.Message,
                    null,
                    ex.Message
                ));
            }
        }
    }
}
