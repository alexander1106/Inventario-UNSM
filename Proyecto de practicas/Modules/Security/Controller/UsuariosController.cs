using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;
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
        try
        {
            var data = await _usuariosService.GetAllAsync();

            return Ok(new ApiResponse<List<UsuarioResponseDTO>>(
                true,
                "Usuarios obtenidos correctamente",
                data
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(false, ex.Message, null));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var usuario = await _usuariosService.GetByIdAsync(id);

            if (usuario == null)
                return NotFound(new ApiResponse<object>(
                    false,
                    "Usuario no encontrado",
                    null
                ));

            return Ok(new ApiResponse<UsuarioResponseDTO>(
                true,
                "Usuario obtenido correctamente",
                usuario
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(false, ex.Message, null));
        }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Add([FromForm] UsuarioCreateDTO dto)
    {
        try
        {
            var nuevo = await _usuariosService.AddAsync(dto);

            return Created("", new ApiResponse<UsuarioResponseDTO>(
                true,
                "Usuario registrado exitosamente",
                nuevo
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(false, ex.Message, null));
        }
    }

    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(int id, [FromForm] UsuarioUpdateDTO dto)
    {
        try
        {
            dto.Id = id;

            var actualizado = await _usuariosService.UpdateAsync(dto);

            if (actualizado == null)
                return NotFound(new ApiResponse<object>(
                    false,
                    "Usuario no encontrado",
                    null
                ));

            return Ok(new ApiResponse<UsuarioResponseDTO>(
                true,
                "Usuario actualizado correctamente",
                actualizado
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(false, ex.Message, null));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var eliminado = await _usuariosService.DeleteAsync(id);

            if (!eliminado)
                return NotFound(new ApiResponse<object>(
                    false,
                    "Usuario no encontrado",
                    null
                ));

            return Ok(new ApiResponse<object>(
                true,
                "Usuario eliminado correctamente",
                null
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(false, ex.Message, null));
        }
    }

    [HttpGet("filtrar")]
    public async Task<IActionResult> Filtrar([FromQuery] UsuarioFiltro filtro)
    {
        try
        {
            var data = await _usuariosService.FiltrarAsync(filtro);

            return Ok(new ApiResponse<List<UsuarioResponseDTO>>(
                true,
                "Usuarios filtrados correctamente",
                data
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(false, ex.Message, null));
        }
    }

    [HttpGet("usuario-actual")]
    public async Task<IActionResult> GetUsuarioActual()
    {
        try
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value
                ?? User.FindFirst("username")?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(username))
                return Unauthorized(new ApiResponse<object>(
                    false,
                    "Usuario no autenticado",
                    null
                ));

            var usuario = await _usuariosService.GetByUsernameAsync(username);

            if (usuario == null)
                return NotFound(new ApiResponse<object>(
                    false,
                    "Usuario no encontrado",
                    null
                ));

            return Ok(new ApiResponse<UsuarioResponseDTO>(
                true,
                "Usuario actual obtenido",
                usuario
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(false, ex.Message, null));
        }
    }

    [HttpGet("paginado")]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var (data, total) = await _usuariosService.GetPagedAsync(page, pageSize);

            return Ok(new ApiResponse<object>(
                true,
                "Usuarios paginados correctamente",
                new
                {
                    data,
                    meta = new
                    {
                        total,
                        page,
                        pageSize,
                        totalPages = (int)Math.Ceiling((double)total / pageSize)
                    }
                }
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(false, ex.Message, null));
        }
    }
}