using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Controller
{
    [Route("api/tipo-ubicacion")]
    [ApiController]
    public class TipoUbicacionController : ControllerBase
    {
        private readonly ITipoUbicacionService _service;
        public TipoUbicacionController(ITipoUbicacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<TipoUbicacion>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<TipoUbicacion>>(
                true,
                "Lista obtenida correctamente",
                result
            ));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TipoUbicacion>>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new ApiResponse<TipoUbicacion>(
                    false,
                    "No se encontró el registro",
                    null
                ));
            }

            return Ok(new ApiResponse<TipoUbicacion>(
                true,
                "Registro encontrado",
                result
            ));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TipoUbicacion>>> Create([FromBody] TipoUbicacion tipoUbicacion)
        {
            try
            {
                var result = await _service.AddAsync(tipoUbicacion);
                return CreatedAtAction(nameof(GetById), new { id = result.Id },
                    new ApiResponse<TipoUbicacion>(
                        true,
                        "Registro creado correctamente",
                        result
                    )
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<TipoUbicacion>(
                    false,
                    ex.Message,
                    null,
                    ex.Message
                ));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TipoUbicacion>>> Update(int id, [FromBody] TipoUbicacion tipoUbicacion)
        {
            try
            {
                var result = await _service.UpdateAsync(id, tipoUbicacion);

                return Ok(new ApiResponse<TipoUbicacion>(
                    true,
                    "Registro actualizado correctamente",
                    result
                ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<TipoUbicacion>(
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
                        "No se encontró el registro",
                        null
                    ));
                }

                return Ok(new ApiResponse<object>(
                    true,
                    "Registro eliminado correctamente",
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