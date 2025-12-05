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

        var nuevo = await _usuariosRepository.CreateAsync(usuario);

        return _mapper.Map<UsuariosDto>(nuevo);
    }

    public async Task<UsuariosDto?> UpdateAsync(UsuariosDto usuarioDto)
    {
        // Obtener el usuario existente
        var existente = await _usuariosRepository.GetByIdAsync(usuarioDto.Id);
        if (existente == null)
            return null;

        // Mapear los datos que se pueden actualizar (nombres, apellidos, email, username)
        existente.Nombre = usuarioDto.Nombre;
        existente.Apellido = usuarioDto.Apellido;
        existente.Email = usuarioDto.Email;
        existente.Username = usuarioDto.Username;

        // Mantener campos obligatorios que no vienen en el DTO
        // existente.Password se mantiene igual
        // existente.Estado se mantiene igual
        // existente.RolId se mantiene igual

        // Guardar cambios
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

    // ✔ Método para obtener la entidad real
    public async Task<Usuario?> GetEntityByUsernameAsync(string usernameActual)
    {
        return await _usuariosRepository.GetByUsernameAsync(usernameActual);
    }

    // ✔ Cambiar contraseña correctamente
    public async Task<bool> CambiarPasswordAsync(int idUsuario, string passwordNueva)
    {
        var usuario = await _usuariosRepository.GetByIdAsync(idUsuario);
        if (usuario == null)
            return false;

        usuario.Password = _passwordHasher.HashPassword(usuario, passwordNueva);

        await _usuariosRepository.UpdatePasswordAsync(usuario.Id, usuario.Password);
        return true;
    }

    // ✔ Verificar password usando entidad Usuario (ya corregido)
    public bool VerificarPassword(Usuario usuario, string passwordPlano)
    {
        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, passwordPlano);
        return resultado == PasswordVerificationResult.Success;
    }
}
