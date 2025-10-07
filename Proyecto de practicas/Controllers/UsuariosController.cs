using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServices _usuariosService;

        public UsuariosController(IUsuariosServices usuariosService)
        {
            _usuariosService = usuariosService;
        }

        // GET: api/usuarios
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
        public async Task<IActionResult> Add([FromBody] UsuariosDto usuarioDto)
        {
            try
            {
                var nuevoUsuario = await _usuariosService.AddAsync(usuarioDto);
                return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.Username }, nuevoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

  
        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuariosDto usuarioDto)
        {
            if (id != usuarioDto.EstadoInt) // 👈 aquí deberías usar el ID real de Usuario
                return BadRequest("ID no coincide");

            try
            {
                var actualizado = await _usuariosService.UpdateAsync(usuarioDto);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        // GET: api/usuarios/usuario-actual
        [HttpGet("usuario-actual")]
        public async Task<IActionResult> GetUsuarioActual()
        {
            var username = User?.Identity?.Name;

            if (string.IsNullOrEmpty(username))
                return Unauthorized(new { message = "No hay usuario autenticado" });

            var usuario = await _usuariosService.GetByUsernameAsync(username);
            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(usuario);
        }


        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _usuariosService.DeleteAsync(id);

            if (!eliminado)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            return Ok(new { mensaje = "Usuario eliminado exitosamente" });
        }

        // POST: api/usuarios/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var valido = await _usuariosService.ValidateLoginAsync(request.Username, request.Password);
            if (!valido) return Unauthorized(new { message = "Usuario o contraseña incorrecta" });

            // Aquí podrías generar un JWT si implementas autenticación
            return Ok(new { message = "Login exitoso" });
        }
    }
}
