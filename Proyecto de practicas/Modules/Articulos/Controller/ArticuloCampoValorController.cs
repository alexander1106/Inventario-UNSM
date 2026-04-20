using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Modules.Articulos.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticuloCampoValorController : ControllerBase
    {
        private readonly IArticuloCampoValorService _service;

        public ArticuloCampoValorController(IArticuloCampoValorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(new ApiResponse<object>(
                true,
                $"Se encontraron {result.Count()} registros",
                result
            ));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    $"No se encontró el registro con ID {id}",
                    null
                ));
            }

            return Ok(new ApiResponse<object>(
                true,
                $"Registro con ID {id} obtenido correctamente",
                result
            ));
        }

        [HttpGet("tipo-articulos/{tipoArticuloId}")]
        public async Task<IActionResult> GetByTipoArticuloId(int tipoArticuloId)
        {
            var result = await _service.GetByTipoArticuloIdAsync(tipoArticuloId);

            return Ok(new ApiResponse<object>(
                true,
                $"Se encontraron {result.Count()} registros para el tipoArticuloId {tipoArticuloId}",
                result
            ));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArticuloCampoValorDto dto)
        {
            await _service.AddAsync(dto);

            return Ok(new ApiResponse<object>(
                true,
                "Registro creado con éxito",
                null
            ));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArticuloCampoValorDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "El id no coincide",
                    null
                ));
            }

            await _service.UpdateAsync(dto);

            return Ok(new ApiResponse<object>(
                true,
                "Registro actualizado con éxito",
                null
            ));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok(new ApiResponse<object>(
                true,
                "Registro eliminado con éxito",
                null
            ));
        }
    }
}