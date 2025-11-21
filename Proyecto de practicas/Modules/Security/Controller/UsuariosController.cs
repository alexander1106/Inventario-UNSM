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

    // 📌 Obtener todos los usuarios
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _usuariosService.GetAllAsync();
        return Ok(usuarios);
    }

    // 📌 Obtener usuario por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _usuariosService.GetByIdAsync(id);
        if (usuario == null) return NotFound(new { mensaje = "Usuario no encontrado" });
        return Ok(usuario);
    }

    // 📌 Crear usuario
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] UsuarioCreateDTO usuarioDto)
    {
        var nuevo = await _usuariosService.AddAsync(usuarioDto);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
    }

    // 📌 Actualizar usuario
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuariosDto usuarioDto)
    {
        usuarioDto.Id = id;
        var actualizado = await _usuariosService.UpdateAsync(usuarioDto);
        return Ok(actualizado);
    }

    // 📌 Eliminar usuario
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _usuariosService.DeleteAsync(id);
        if (!eliminado) return NotFound(new { mensaje = "Usuario no encontrado" });
        return Ok(new { mensaje = "Usuario eliminado exitosamente" });
    }

    // 📌 Obtener usuario actual pasando username en query
    [Authorize]   // ✔ Este sí
    [HttpGet("usuario-actual")]
    public async Task<IActionResult> GetUsuarioActual()
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(username))
            return Unauthorized(new { mensaje = "Usuario no autenticado" });

        var usuario = await _usuariosService.GetByUsernameAsync(username);

        if (usuario == null)
            return NotFound(new { mensaje = "Usuario no encontrado" });

        return Ok(usuario);
    }

}
