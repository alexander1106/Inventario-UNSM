namespace Proyecto_de_practicas.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    [HttpGet("publico")]
    public IActionResult Publico() => Ok("Cualquiera puede entrar");

    [HttpGet("privado")]
    [Authorize]
    public IActionResult Privado() => Ok("Solo usuarios con JWT válido");
}
