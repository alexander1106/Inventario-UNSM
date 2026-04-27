using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_de_practicas.Modules.Articulos.Controller
{
    [Route("api/articulos")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _service;

        public ArticuloController(IArticuloService service)
        {
            _service = service;
        }

        [HttpGet("con-campos")]
        public async Task<IActionResult> GetAllConCampos()
        {
            var result = await _service.GetAllConCamposAsync();

            return Ok(new ApiResponse<object>(
                true,
                $"Se encontraron {result.Count} artículos con campos",
                result
            ));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(new ApiResponse<object>(
                true,
                $"Se encontraron {result.Count} artículos",
                result
            ));
        }
        [HttpPut("update-con-campos/{id}")]
        public async Task<IActionResult> UpdateConCampos(int id, [FromBody] ArticuloConCamposRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Datos inválidos",
                    null
                ));
            }

            var result = await _service.UpdateArticuloConCampos(id, request);

            return Ok(new ApiResponse<object>(
                true,
                $"Artículo ID {id} actualizado correctamente con campos",
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
                    $"No se encontró el artículo con ID {id}",
                    null
                ));
            }

            return Ok(new ApiResponse<object>(
                true,
                $"Artículo con ID {id} obtenido correctamente",
                result
            ));
        }

        [HttpPost("guardar-con-campos")]
        public async Task<IActionResult> GuardarArticuloConCampos([FromBody] ArticuloConCamposRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Datos del artículo inválidos",
                    null
                ));
            }

            var mensaje = await _service.GuardarArticuloConCampos(request);

            return Ok(new ApiResponse<object>(
                true,
                mensaje,
                null
            ));
        }

        [HttpGet("qr/{codigoCorto}")]
        public async Task<IActionResult> GetByCodigoCorto(string codigoCorto)
        {
            if (string.IsNullOrEmpty(codigoCorto))
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Código QR inválido",
                    null
                ));
            }

            var articulo = await _service.GetByCodigoCortoAsync(codigoCorto);

            if (articulo == null)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    $"No se encontró el artículo con código {codigoCorto}",
                    null
                ));
            }

            return Ok(new ApiResponse<object>(
                true,
                $"Artículo {codigoCorto} encontrado correctamente",
                articulo
            ));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArticuloDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            return Ok(new ApiResponse<object>(
                true,
                $"Artículo ID {id} actualizado correctamente",
                result
            ));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    $"No se pudo eliminar el artículo ID {id}",
                    null
                ));
            }

            return Ok(new ApiResponse<object>(
                true,
                $"Artículo ID {id} eliminado correctamente",
                id
            ));
        }

        [HttpGet("campos/{tipoArticuloId}")]
        public async Task<IActionResult> GetCamposPorTipoArticulo(int tipoArticuloId)
        {
            var campos = await _service.GetCamposPorTipoArticuloAsync(tipoArticuloId);

            return Ok(new ApiResponse<object>(
                true,
                $"Campos obtenidos para tipoArticuloId {tipoArticuloId}",
                campos
            ));
        }

        [HttpGet("pivot/tipo/{tipoArticuloId}")]
        public async Task<IActionResult> GetPivotPorTipoArticulo(int tipoArticuloId)
        {
            var result = await _service.GetArticulosPivotPorTipoAsync(tipoArticuloId);

            return Ok(new ApiResponse<object>(
                true,
                $"Datos pivot obtenidos para tipoArticuloId {tipoArticuloId}",
                result
            ));
        }
    }
}