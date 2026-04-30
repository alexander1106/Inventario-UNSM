using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Modules.Articulos.Controller
{
    [Route("api/campos-articulo")]
    [ApiController]
    public class CampoArticuloController : ControllerBase
    {
        private readonly ICampoArticuloService _service;

        public CampoArticuloController(ICampoArticuloService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CampoArticuloDto>>>> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(new ApiResponse<List<CampoArticuloDto>>(
                true,
                "Lista obtenida correctamente",
                result
            ));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CampoArticuloDto>>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new ApiResponse<CampoArticuloDto>(
                    false,
                    "El campo del artículo no fue encontrado",
                    null
                ));
            }

            return Ok(new ApiResponse<CampoArticuloDto>(
                true,
                "OK",
                result
            ));
        }

        [HttpGet("tipo-articulo/{tipoArticuloId}")]
        public async Task<ActionResult<ApiResponse<List<CampoArticuloDto>>>> GetByTipoArticulo(int tipoArticuloId)
        {
            var result = await _service.GetByTipoArticuloIdAsync(tipoArticuloId);

            return Ok(new ApiResponse<List<CampoArticuloDto>>(
                true,
                "Datos filtrados correctamente",
                result
            ));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CampoArticuloDto>>> Create(CampoArticuloDto dto)
        {
            try
            {
                var result = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = result.Id },
                    new ApiResponse<CampoArticuloDto>(
                        true,
                        "Creado correctamente",
                        result
                    ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<CampoArticuloDto>(
                    false,
                    ex.Message,
                    null
                ));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<CampoArticuloDto>>> Update(int id, CampoArticuloDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);

                return Ok(new ApiResponse<CampoArticuloDto>(
                    true,
                    "Actualizado correctamente",
                    result
                ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<CampoArticuloDto>(
                    false,
                    ex.Message,
                    null
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
                        "El campo del artículo no existe o ya fue eliminado",
                        null
                    ));
                }

                return Ok(new ApiResponse<object>(
                    true,
                    "Eliminado correctamente",
                    null
                ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    ex.Message,
                    null
                ));
            }
        }

        [HttpPost("lote")]
        public async Task<ActionResult<ApiResponse<object>>> CreateMultiple(List<CampoArticuloDto> campos)
        {
            try
            {
                foreach (var campo in campos)
                {
                    await _service.AddAsync(campo);
                }

                return Ok(new ApiResponse<object>(
                    true,
                    "Campos creados correctamente",
                    null
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
    }
}