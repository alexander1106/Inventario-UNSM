using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Security.Services.IServices;
using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Controller
{
    [Route("api/security")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuariosServices _usuariosService;

        public AuthController(IUsuariosServices usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var usuario = await _usuariosService.GetEntityByUsernameAsync(login.Username);

            if (usuario == null)
                return Unauthorized(new { message = "Credenciales incorrectos" });

            bool esValido = _usuariosService.VerificarPassword(usuario, login.Password);

            if (!esValido)
                return Unauthorized(new { message = "Credenciales incorrectos" });

            return Ok(new
            {
                message = "Login exitoso",
                usuario = new
                {
                    id = usuario.Id,
                    nombre = usuario.Nombre,
                    apellido = usuario.Apellido,
                    email = usuario.Email,
                    username = usuario.Username,
                    rolId = usuario.RolId,
                    estado = usuario.Estado
                }
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Sesión cerrada" });
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}