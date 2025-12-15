using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Modules.Articulos.Controller
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

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<TipoArticuloDTO>> Create([FromForm] TipoArticuloDTO dtoEntrada)
        {
            try
            {
                // Validar duplicado
                var existentes = await _service.GetAllAsync();
                if (existentes.Any(t => t.Nombre.ToLower() == dtoEntrada.Nombre.ToLower()))
                {
                    return BadRequest(new
                    {
                        message = "Ya existe un tipo de artículo con ese nombre."
                    });
                }

                string? rutaImagen = null;

                if (dtoEntrada.Imagen != null && dtoEntrada.Imagen.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid() + Path.GetExtension(dtoEntrada.Imagen.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await dtoEntrada.Imagen.CopyToAsync(stream);

                    rutaImagen = "/imagenes/" + uniqueFileName;
                }

                dtoEntrada.ImagenPath = rutaImagen;

                var result = await _service.AddAsync(dtoEntrada);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<TipoArticuloDTO>> Update(int id, [FromForm] TipoArticuloDTO dtoEntrada)
        {
            try
            {
                string? rutaImagen = null;

                if (dtoEntrada.Imagen != null && dtoEntrada.Imagen.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid() + Path.GetExtension(dtoEntrada.Imagen.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await dtoEntrada.Imagen.CopyToAsync(stream);

                    rutaImagen = "/imagenes/" + uniqueFileName;
                }

                if (rutaImagen != null)
                    dtoEntrada.ImagenPath = rutaImagen;

                var result = await _service.UpdateAsync(id, dtoEntrada);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                    return NotFound(new { message = "No se encontró el tipo de artículo." });

                // ✅ Retornar mensaje en lugar de 204
                return Ok(new { message = "Artículo eliminado exitosamente." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}/encabezado")]
        public async Task<ActionResult<List<string>>> GetEncabezado(int id)
        {
            var result = await _service.GetEncabezadoArticulosAsync(id);
            if (result == null || !result.Any())
                return NotFound(new { message = "No se encontraron encabezados para este tipo." });

            return Ok(result);
        }

    }
}
