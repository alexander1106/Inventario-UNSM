using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
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
        public async Task<ActionResult<ApiResponse<List<TipoArticuloDTO>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<TipoArticuloDTO>>(
                true,
                "Lista obtenida correctamente",
                result
            ));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TipoArticuloDTO>>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new ApiResponse<TipoArticuloDTO>(
                    false,
                    "No se encontró el tipo de artículo",
                    null
                ));
            }

            return Ok(new ApiResponse<TipoArticuloDTO>(
                true,
                "Tipo de artículo encontrado",
                result
            ));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponse<TipoArticuloDTO>>> Create([FromForm] TipoArticuloDTO dtoEntrada)
        {
            try
            {
                var existentes = await _service.GetAllAsync();
                if (existentes.Any(t => t.Nombre.ToLower() == dtoEntrada.Nombre.ToLower()))
                {
                    return BadRequest(new ApiResponse<TipoArticuloDTO>(
                        false,
                        "Ya existe un tipo de artículo con ese nombre",
                        null
                    ));
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
                return CreatedAtAction(nameof(GetById), new { id = result.Id },
                    new ApiResponse<TipoArticuloDTO>(
                        true,
                        "Tipo de artículo creado correctamente",
                        result
                    )
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<TipoArticuloDTO>(
                    false,
                    ex.Message,
                    null,
                    ex.Message
                ));
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponse<TipoArticuloDTO>>> Update(int id, [FromForm] TipoArticuloDTO dtoEntrada)
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

                return Ok(new ApiResponse<TipoArticuloDTO>(
                    true,
                    "Tipo de artículo actualizado correctamente",
                    result
                ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<TipoArticuloDTO>(
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
                        "No se encontró el tipo de artículo",
                        null
                    ));
                }

                return Ok(new ApiResponse<object>(
                    true,
                    "Artículo eliminado exitosamente",
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

        [HttpGet("{id}/encabezado")]
        public async Task<ActionResult<ApiResponse<List<string>>>> GetEncabezado(int id)
        {
            var result = await _service.GetEncabezadoArticulosAsync(id);

            if (result == null || !result.Any())
            {
                return NotFound(new ApiResponse<List<string>>(
                    false,
                    "No se encontraron encabezados para este tipo",
                    null
                ));
            }

            return Ok(new ApiResponse<List<string>>(
                true,
                "Encabezados obtenidos correctamente",
                result
            ));
        }
    }
}