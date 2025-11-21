using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuariosServices _usuariosService;
    private readonly IConfiguration _config;

    public AuthController(IUsuariosServices usuariosService, IConfiguration config)
    {
        _usuariosService = usuariosService;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Validar login usando tu servicio
        var valido = await _usuariosService.ValidateLoginAsync(request.Username, request.Password);

        if (!valido)
            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

        // Generar token JWT
        var token = GenerateToken(request.Username);

        return Ok(new { token });
    }

    private string GenerateToken(string username)
    {
        var claims = new[]
           {
            new Claim(ClaimTypes.NameIdentifier, username), // NECESARIO
            new Claim(ClaimTypes.Name, username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

// DTO para login
public class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}