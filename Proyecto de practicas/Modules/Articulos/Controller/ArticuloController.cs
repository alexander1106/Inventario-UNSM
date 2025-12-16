using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<List<ArticuloDto>>> GetAllConCampos()
        {
            var result = await _service.GetAllConCamposAsync();
            return Ok(result);
        }

        // 🔹 Obtener todos
        [HttpGet]
        public async Task<ActionResult<List<ArticuloDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // 🔹 Obtener por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticuloDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("guardar-con-campos")]
        public async Task<ActionResult> GuardarArticuloConCampos([FromBody] ArticuloConCamposRequest request)
        {
            if (request == null)
                return BadRequest("Datos del artículo inválidos");

            var mensaje = await _service.GuardarArticuloConCampos(request);

            return Ok(new { mensaje });
        }
        // 🔹 Obtener artículo por QR
        [HttpGet("qr/{codigoCorto}")]
        public async Task<ActionResult<ArticuloDto>> GetByCodigoCorto(string codigoCorto)
        {
            if (string.IsNullOrEmpty(codigoCorto))
                return BadRequest("Código QR inválido");

            var articulo = await _service.GetByCodigoCortoAsync(codigoCorto);
            if (articulo == null)
                return NotFound("Artículo no encontrado");

            return Ok(articulo);
        }

        // 🔹 Actualizar artículo
        [HttpPut("{id}")]
        public async Task<ActionResult<ArticuloDto>> Update(int id, [FromBody] ArticuloDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        // 🔹 Eliminar artículo
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
        // 🔹 Obtener campos dinámicos de un tipo de artículo
        [HttpGet("campos/{tipoArticuloId}")]
        public async Task<ActionResult<List<CampoArticuloDto>>> GetCamposPorTipoArticulo(int tipoArticuloId)
        {
            var campos = await _service.GetCamposPorTipoArticuloAsync(tipoArticuloId);
            return Ok(campos);
        }

        // 🔹 Obtener artículos pivot por tipo
        [HttpGet("pivot/tipo/{tipoArticuloId}")]
        public async Task<ActionResult<List<Dictionary<string, object>>>> GetPivotPorTipoArticulo(int tipoArticuloId)
        {
            var result = await _service.GetArticulosPivotPorTipoAsync(tipoArticuloId);
            return Ok(result);
        }
    }

}