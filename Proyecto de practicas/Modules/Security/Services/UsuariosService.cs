using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

public class UsuariosService : IUsuariosServices
{
    private readonly IUsuariosRepository _usuariosRepository;
    private readonly PasswordHasher<Usuario> _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<UsuariosService> _logger;


    public UsuariosService(
        IUsuariosRepository usuariosRepository,
        IMapper mapper,
        ILogger<UsuariosService> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _usuariosRepository = usuariosRepository;
        _mapper = mapper;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _passwordHasher = new PasswordHasher<Usuario>();
    }

    private string ConstruirUrl(string? path)
    {
        if (string.IsNullOrEmpty(path)) return "";

        var request = _httpContextAccessor.HttpContext.Request;
        return $"{request.Scheme}://{request.Host}/{path}";
    }

    private async Task<string?> GuardarImagenAsync(IFormFile? imagen, string carpeta)
    {
        if (imagen == null || imagen.Length == 0) return null;

        string nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(imagen.FileName)}";
        string rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", carpeta);

        if (!Directory.Exists(rutaCarpeta))
            Directory.CreateDirectory(rutaCarpeta);

        string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            await imagen.CopyToAsync(stream);
        }

        return Path.Combine(carpeta, nombreArchivo).Replace("\\", "/");
    }

    public async Task<List<UsuarioResponseDTO>> GetAllAsync()
    {
        var usuarios = await _usuariosRepository.GetAllAsync();

        return usuarios.Select(u =>
        {
            var dto = _mapper.Map<UsuarioResponseDTO>(u);
            dto.ImagenUrl = ConstruirUrl(u.ImagenPath);
            return dto;
        }).ToList();
    }

    public async Task<UsuarioResponseDTO?> GetByIdAsync(int id)
    {
        var usuario = await _usuariosRepository.GetByIdAsync(id);
        if (usuario == null) return null;

        var dto = _mapper.Map<UsuarioResponseDTO>(usuario);
        dto.ImagenUrl = ConstruirUrl(usuario.ImagenPath);

        return dto;
    }

    public async Task<UsuarioResponseDTO> AddAsync(UsuarioCreateDTO usuarioDto)
    {
        try
        {
            _logger.LogInformation("Intentando registrar usuario {Username}", usuarioDto.Username);

            var existente = await _usuariosRepository.GetByUsernameAsync(usuarioDto.Username);
            if (existente != null)
            {
                _logger.LogWarning("Username {Username} ya existe", usuarioDto.Username);
                throw new Exception("El username ya existe");
            }

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            usuario.Password = _passwordHasher.HashPassword(usuario, usuarioDto.Password);
            usuario.FechaCreacion = DateTime.UtcNow;
            usuario.UsuarioCreacion = username ?? "Sistema";
            usuario.Estado = true;
            usuario.ImagenPath = await GuardarImagenAsync(usuarioDto.Imagen, "usuarios");

            var nuevo = await _usuariosRepository.CreateAsync(usuario);

            _logger.LogInformation("Usuario creado con ID {Id}", nuevo.Id);

            var dto = _mapper.Map<UsuarioResponseDTO>(nuevo);
            dto.ImagenUrl = ConstruirUrl(nuevo.ImagenPath);

            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar usuario {Username}", usuarioDto.Username);
            throw;
        }
    }
    public async Task<UsuarioResponseDTO?> UpdateAsync(UsuarioUpdateDTO dto)
    {
        _logger.LogInformation("Intentando actualizar usuario ID {Id}", dto.Id);

        var existente = await _usuariosRepository.GetByIdAsync(dto.Id);
        if (existente == null)
            return null;

        existente.Nombre = dto.Nombre ?? existente.Nombre;
        existente.Apellido = dto.Apellido ?? existente.Apellido;
        existente.Email = dto.Email ?? existente.Email;
        existente.Username = dto.Username ?? existente.Username;
        existente.Estado = dto.Estado ?? existente.Estado;
        existente.RolId = dto.RolId ?? existente.RolId;

        if (dto.Imagen != null)
            existente.ImagenPath = await GuardarImagenAsync(dto.Imagen, "usuarios");

        // 🔥 AUDITORÍA
        var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        existente.FechaModificacion = DateTime.UtcNow;
        existente.UsuarioModificacion = username ?? "Sistema";

        var actualizado = await _usuariosRepository.UpdateAsync(existente);

        var response = _mapper.Map<UsuarioResponseDTO>(actualizado);
        response.ImagenUrl = ConstruirUrl(actualizado.ImagenPath);

        return response;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Intentando eliminar usuario ID {Id}", id);

        var eliminado = await _usuariosRepository.DeleteAsync(id);

        if (!eliminado)
        {
            _logger.LogWarning("No se encontró usuario ID {Id} para eliminar", id);
            return false;
        }

        _logger.LogInformation("Usuario ID {Id} eliminado correctamente", id);

        return true;
    }
    public async Task<UsuarioResponseDTO?> GetByUsernameAsync(string usernameActual)
    {
        var usuario = await _usuariosRepository.GetByUsernameAsync(usernameActual);
        if (usuario == null) return null;

        var dto = _mapper.Map<UsuarioResponseDTO>(usuario);
        dto.ImagenUrl = ConstruirUrl(usuario.ImagenPath);

        return dto;
    }

    public async Task<bool> ValidateLoginAsync(string username, string passwordIngresado)
    {
        var usuario = await _usuariosRepository.GetByUsernameAsync(username);
        if (usuario == null) return false;

        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, passwordIngresado);
        return resultado == PasswordVerificationResult.Success;
    }

    public async Task<Usuario?> GetEntityByUsernameAsync(string usernameActual)
    {
        return await _usuariosRepository.GetByUsernameAsync(usernameActual);
    }

    public async Task<bool> CambiarPasswordAsync(int idUsuario, string passwordNueva)
    {
        var usuario = await _usuariosRepository.GetByIdAsync(idUsuario);
        if (usuario == null) return false;

        usuario.Password = _passwordHasher.HashPassword(usuario, passwordNueva);

        await _usuariosRepository.UpdatePasswordAsync(usuario.Id, usuario.Password);
        return true;
    }
    public async Task<List<UsuarioResponseDTO>> FiltrarAsync(UsuarioFiltro filtro)
    {
        var usuarios = await _usuariosRepository.FiltrarAsync(filtro);

        return usuarios.Select(u =>
        {
            var dto = _mapper.Map<UsuarioResponseDTO>(u);
            dto.ImagenUrl = ConstruirUrl(u.ImagenPath);
            return dto;
        }).ToList();
    }
    public async Task<(List<UsuarioResponseDTO>, int)> GetPagedAsync(int page, int pageSize)
    {
        var (usuarios, total) = await _usuariosRepository.GetPagedAsync(page, pageSize);

        var data = usuarios.Select(u =>
        {
            var dto = _mapper.Map<UsuarioResponseDTO>(u);
            dto.ImagenUrl = ConstruirUrl(u.ImagenPath);
            return dto;
        }).ToList();

        return (data, total);
    }
    public bool VerificarPassword(Usuario usuario, string passwordPlano)
    {
        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, passwordPlano);
        return resultado == PasswordVerificationResult.Success;
    }

    public async Task<bool> ActualizarImagenAsync(int usuarioId, IFormFile imagen)
    {
        if (imagen == null || imagen.Length == 0)
            return false;

        var usuario = await _usuariosRepository.GetByIdAsync(usuarioId);
        if (usuario == null) return false;

        var nuevaRuta = await GuardarImagenAsync(imagen, "usuarios");
        if (string.IsNullOrEmpty(nuevaRuta)) return false;

        usuario.ImagenPath = nuevaRuta;

        await _usuariosRepository.UpdateAsync(usuario);

        return true;
    }
}