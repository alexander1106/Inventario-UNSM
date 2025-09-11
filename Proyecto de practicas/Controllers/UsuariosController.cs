using System.Threading.Tasks;
using global::Proyecto_de_practicas.Models;
using global::Proyecto_de_practicas.Service;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuariosService.GetAllAsync();
            return Ok(usuarios);
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuariosService.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Usuario usuario)
        {
            try
            {
                var nuevoUsuario = await _usuariosService.AddAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.Id }, nuevoUsuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest("ID no coincide");

            try
            {
                var actualizado = await _usuariosService.UpdateAsync(usuario);
                return Ok(actualizado);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _usuariosService.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }

        // POST: api/usuarios/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var valido = await _usuariosService.ValidateLoginAsync(request.Username, request.Password);
            if (!valido) return Unauthorized(new { message = "Usuario o contraseña incorrecta" });

            // Aquí podrías generar JWT si quieres autenticación
            return Ok(new { message = "Login exitoso" });
        }
    }

}
