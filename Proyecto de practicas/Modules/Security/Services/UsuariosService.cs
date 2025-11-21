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

    public async Task<UsuariosDto> AddAsync(UsuarioCreateDTO usuarioDto)
    {
        var existente = await _usuariosRepository.GetByUsernameAsync(usuarioDto.Username);
        if (existente != null)
            throw new Exception("Ya existe un usuario con ese nombre");

        var usuario = _mapper.Map<Usuario>(usuarioDto);
        usuario.Password = _passwordHasher.HashPassword(usuario, usuario.Password);

        var nuevo = await _usuariosRepository.CreateAsync(usuario);
        return _mapper.Map<UsuariosDto>(nuevo);
    }

    public async Task<UsuariosDto> UpdateAsync(UsuariosDto usuarioDto)
    {
        var existente = await _usuariosRepository.GetByIdAsync(usuarioDto.Id);
        if (existente == null)
            throw new Exception("Usuario no encontrado");

        var usuario = _mapper.Map<Usuario>(usuarioDto);

        if (!string.IsNullOrEmpty(usuario.Password) && usuario.Password != existente.Password)
            usuario.Password = _passwordHasher.HashPassword(usuario, usuario.Password);
        else
            usuario.Password = existente.Password;

        var actualizado = await _usuariosRepository.UpdateAsync(usuario);
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

        var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, passwordIngresado);
        return result == PasswordVerificationResult.Success;
    }
}
