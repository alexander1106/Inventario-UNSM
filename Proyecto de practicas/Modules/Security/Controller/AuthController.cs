using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

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
            var valido = await _usuariosService.ValidateLoginAsync(login.Username, login.Password);
            if (!valido)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

            // Aquí NO usas JWT, así que simplemente devuelves éxito
            return Ok(new { message = "Login exitoso", username = login.Username });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // No hace nada especial porque tu login es manual,
            // pero existe para que el frontend pueda llamarlo.
            return Ok(new { message = "Sesión cerrada" });
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
