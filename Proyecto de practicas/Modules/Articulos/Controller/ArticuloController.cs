using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Service;

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

        // 🔹 Por tipo
        [HttpGet("tipo/{tipoArticuloId}")]
        public async Task<ActionResult<List<ArticuloDto>>> GetByTipoArticulo(int tipoArticuloId)
        {
            var result = await _service.GetByTipoArticuloIdAsync(tipoArticuloId);
            return Ok(result);
        }

        // 🔹 Por ubicación
        [HttpGet("ubicacion/{ubicacionId}")]
        public async Task<ActionResult<List<ArticuloDto>>> GetByUbicacion(int ubicacionId)
        {
            var result = await _service.GetByUbicacionIdAsync(ubicacionId);
            return Ok(result);
        }

        // 🔵 Crear SOLO artículo (básico)
        [HttpPost]
        public async Task<ActionResult<ArticuloDto>> Create([FromBody] ArticuloDto dto)
        {
            var articulo = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = articulo.Id }, articulo);
        }

        // 🔥 Crear Artículo + sus campos dinámicos
        [HttpPost("crear-con-campos")]
        public async Task<ActionResult> CreateArticuloCompleto([FromBody] ArticuloDto request)
        {
            var msg = await _service.CreateArticuloConCampos(request);
            return Ok(new { mensaje = msg });
        }

        [HttpPut("update-con-campos/{id}")]
        public async Task<ActionResult> UpdateArticuloCompleto([FromBody] ArticuloDto request)
        {
            var msg = await _service.UpdateArticuloConCampos(request);
            return Ok(new { mensaje = msg });
        }


        // 🔹 Actualizar
        [HttpPut("{id}")]
        public async Task<ActionResult<ArticuloDto>> Update(int id, ArticuloDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        // 🔹 Obtener artículos pivot por tipo
        [HttpGet("pivot/tipo/{tipoArticuloId}")]
        public async Task<ActionResult<List<Dictionary<string, object>>>> GetPivotPorTipoArticulo(int tipoArticuloId)
        {
            var result = await _service.GetArticulosPivotPorTipoAsync(tipoArticuloId);
            return Ok(result);
        }

        // 🔹 Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
