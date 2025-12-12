using AutoMapper;
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

    public UsuariosService(IUsuariosRepository usuariosRepository, IMapper mapper)
    {
        _usuariosRepository = usuariosRepository;
        _mapper = mapper;
        _passwordHasher = new PasswordHasher<Usuario>();
    }

    // 🟢 Método auxiliar para guardar la imagen en disco
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

        // Retorna la ruta relativa para almacenar en la DB
        return Path.Combine(carpeta, nombreArchivo).Replace("\\", "/");
    }

    public async Task<List<UsuariosDto>> GetAllAsync()
    {
        var usuarios = await _usuariosRepository.GetAllAsync();
        return _mapper.Map<List<UsuariosDto>>(usuarios);
    }

    public async Task<UsuariosDto?> GetByIdAsync(int id)
    {
        var usuario = await _usuariosRepository.GetByIdAsync(id);
        return usuario == null ? null : _mapper.Map<UsuariosDto>(usuario);
    }

    public async Task<UsuariosDto?> AddAsync(UsuarioCreateDTO usuarioDto)
    {
        var existente = await _usuariosRepository.GetByUsernameAsync(usuarioDto.Username);
        if (existente != null)
            throw new Exception("El username ya existe");

        var usuario = _mapper.Map<Usuario>(usuarioDto);

        usuario.Password = _passwordHasher.HashPassword(usuario, usuario.Password);

        // 🟢 Guardar imagen si se envía
        usuario.ImagenPath = await GuardarImagenAsync(usuarioDto.Imagen, "usuarios");

        var nuevo = await _usuariosRepository.CreateAsync(usuario);

        return _mapper.Map<UsuariosDto>(nuevo);
    }

    public async Task<UsuariosDto?> UpdateAsync(UsuariosDto usuarioDto)
    {
        var existente = await _usuariosRepository.GetByIdAsync(usuarioDto.Id);
        if (existente == null)
            return null;

        existente.Nombre = usuarioDto.Nombre;
        existente.Apellido = usuarioDto.Apellido;
        existente.Email = usuarioDto.Email;
        existente.Username = usuarioDto.Username;

        // 🟢 Guardar nueva imagen si se envía
        if (usuarioDto.Imagen != null)
        {
            existente.ImagenPath = await GuardarImagenAsync(usuarioDto.Imagen, "usuarios");
        }

        var actualizado = await _usuariosRepository.UpdateAsync(existente);
        return _mapper.Map<UsuariosDto>(actualizado);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _usuariosRepository.DeleteAsync(id);
    }

    public async Task<bool> ValidateLoginAsync(string username, string passwordIngresado)
    {
        var usuario = await _usuariosRepository.GetByUsernameAsync(username);
        if (usuario == null) return false;

        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, passwordIngresado);
        return resultado == PasswordVerificationResult.Success;
    }

    public async Task<UsuariosDto?> GetUsuarioActualAsync(string username)
    {
        var usuario = await _usuariosRepository.GetByUsernameAsync(username);
        return usuario == null ? null : _mapper.Map<UsuariosDto>(usuario);
    }

    public async Task<UsuariosDto?> GetByUsernameAsync(string usernameActual)
    {
        var usuario = await _usuariosRepository.GetByUsernameAsync(usernameActual);
        return usuario == null ? null : _mapper.Map<UsuariosDto>(usuario);
    }

    public async Task<Usuario?> GetEntityByUsernameAsync(string usernameActual)
    {
        return await _usuariosRepository.GetByUsernameAsync(usernameActual);
    }

    public async Task<bool> CambiarPasswordAsync(int idUsuario, string passwordNueva)
    {
        var usuario = await _usuariosRepository.GetByIdAsync(idUsuario);
        if (usuario == null)
            return false;

        usuario.Password = _passwordHasher.HashPassword(usuario, passwordNueva);

        await _usuariosRepository.UpdatePasswordAsync(usuario.Id, usuario.Password);
        return true;
    }

    public bool VerificarPassword(Usuario usuario, string passwordPlano)
    {
        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, passwordPlano);
        return resultado == PasswordVerificationResult.Success;
    }

    // 🟢 Actualizar solo imagen del usuario logueado
    public async Task<bool> ActualizarImagenAsync(int usuarioId, IFormFile imagen)
    {
        if (imagen == null || imagen.Length == 0)
            return false;

        var usuario = await _usuariosRepository.GetByIdAsync(usuarioId);
        if (usuario == null) return false;

        // Guardar imagen usando tu método existente
        var nuevaRuta = await GuardarImagenAsync(imagen, "usuarios");
        if (string.IsNullOrEmpty(nuevaRuta)) return false;

        usuario.ImagenPath = nuevaRuta;

        // Actualizar en la DB
        await _usuariosRepository.UpdateAsync(usuario);

        return true;
    }

}
