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
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] UsuarioCreateDTO usuarioDto)
    {
        var nuevo = await _usuariosService.AddAsync(usuarioDto);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuariosDto usuarioDto)
    {
        usuarioDto.Id = id;
        var actualizado = await _usuariosService.UpdateAsync(usuarioDto);
        return Ok(actualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _usuariosService.DeleteAsync(id);
        if (!eliminado) return NotFound(new { mensaje = "Usuario no encontrado" });
        return Ok(new { mensaje = "Usuario eliminado exitosamente" });
    }
}
