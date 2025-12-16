using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuariosServices _usuariosService;

    public UsuariosController(IUsuariosServices usuariosService)
    {
        _usuariosService = usuariosService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _usuariosService.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _usuariosService.GetByIdAsync(id);
        if (usuario == null)
            return NotFound(new { mensaje = "Usuario no encontrado" });

        return Ok(usuario);
    }

    // ✨ Crear usuario con imagen
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Add([FromForm] UsuarioCreateDTO usuarioDto)
    {
        if (usuarioDto == null)
            return BadRequest(new { mensaje = "El cuerpo de la solicitud es nulo" });

        try
        {
            var nuevo = await _usuariosService.AddAsync(usuarioDto);

            if (nuevo == null)
                return BadRequest(new { mensaje = "No se pudo crear el usuario" });

            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, new
            {
                mensaje = "Usuario registrado exitosamente",
                usuario = nuevo
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    // ✨ Actualizar usuario con imagen
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(int id, [FromForm] UsuariosDto usuarioDto)
    {
        usuarioDto.Id = id;

        var actualizado = await _usuariosService.UpdateAsync(usuarioDto);
        if (actualizado == null)
            return NotFound(new { mensaje = "Usuario no encontrado" });

        return Ok(actualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _usuariosService.DeleteAsync(id);
        if (!eliminado)
            return NotFound(new { mensaje = "Usuario no encontrado" });

        return Ok(new { mensaje = "Usuario eliminado exitosamente" });
    }

    // ✨ Obtener usuario autenticado
    [HttpGet("usuario-actual")]
    public async Task<IActionResult> GetUsuarioActual()
    {
        var username =
            User.FindFirst(ClaimTypes.Name)?.Value ??
            User.FindFirst("username")?.Value ??
            User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(username))
            return Unauthorized(new { mensaje = "Usuario no autenticado" });

        var usuario = await _usuariosService.GetByUsernameAsync(username);

        if (usuario == null)
            return NotFound(new { mensaje = "Usuario no encontrado" });

        return Ok(usuario);
    }

    // ✨ Cambiar contraseña
    [HttpPost("cambiar-password")]
    public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDto dto)
    {
        var username =
            User.FindFirst(ClaimTypes.Name)?.Value ??
            User.FindFirst("username")?.Value ??
            User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(username))
            return Unauthorized(new { mensaje = "Usuario no autenticado" });

        var usuarioEntidad = await _usuariosService.GetEntityByUsernameAsync(username);

        if (usuarioEntidad == null)
            return NotFound(new { mensaje = "Usuario no encontrado" });

        bool esValida = _usuariosService.VerificarPassword(usuarioEntidad, dto.PasswordActual);

        if (!esValida)
            return BadRequest(new { mensaje = "La contraseña actual es incorrecta" });

        bool actualizado = await _usuariosService.CambiarPasswordAsync(usuarioEntidad.Id, dto.PasswordNueva);

        if (!actualizado)
            return StatusCode(500, new { mensaje = "Error al cambiar la contraseña" });

        return Ok(new { mensaje = "Contraseña cambiada exitosamente" });
    }
    // ✨ Actualizar solo la imagen del usuario autenticado
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPatch("actualizar-imagen")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ActualizarImagen([FromForm] IFormFile imagen)
    {
        if (imagen == null || imagen.Length == 0)
            return BadRequest(new { mensaje = "No se subió ninguna imagen." });

        var username = User.FindFirst(ClaimTypes.Name)?.Value
                       ?? User.FindFirst("username")?.Value
                       ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(username))
            return Unauthorized(new { mensaje = "Usuario no autenticado" });

        var usuario = await _usuariosService.GetByUsernameAsync(username);
        if (usuario == null)
            return NotFound(new { mensaje = "Usuario no encontrado" });

        var resultado = await _usuariosService.ActualizarImagenAsync(usuario.Id, imagen);
        if (!resultado)
            return StatusCode(500, new { mensaje = "No se pudo actualizar la imagen" });

        return Ok(new { imagenPath = usuario.ImagenPath });
    }



}
