using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [Route("api/tipo-articulo")]
    [ApiController]
    public class TipoArticuloController : ControllerBase
    {
        private readonly ITipoArticuloService _service;

        public TipoArticuloController(ITipoArticuloService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoArticuloDTO>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoArticuloDTO>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}/articulo")]
        public async Task<ActionResult<TipoArticuloDTO>> filtrarPorTipoArticulo(int id)
        {
            var dto = await _service.ObtenerPorIdAsync(id);
            if (dto == null) return NotFound();

            return Ok(dto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<TipoArticuloDTO>> Create([FromForm] TipoArticuloDTO dtoEntrada)
        {
            var existentes = await _service.GetAllAsync();
            if (existentes.Any(t => t.Nombre.ToLower() == dtoEntrada.Nombre.ToLower()))
            {
                return BadRequest("Ya existe un tipo de artículo con ese nombre.");
            }

            string? rutaImagen = null;

            if (dtoEntrada.Imagen != null && dtoEntrada.Imagen.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dtoEntrada.Imagen.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dtoEntrada.Imagen.CopyToAsync(stream);
                }

                rutaImagen = "/imagenes/" + uniqueFileName;
            }

            // 📝 Crear DTO final para el servicio
            var dto = new TipoArticuloDTO
            {
                Nombre = dtoEntrada.Nombre,
                Descripcion = dtoEntrada.Descripcion,
                ImagenPath = rutaImagen
            };

            var result = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<TipoArticuloDTO>> Update(int id, [FromBody] TipoArticuloDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
