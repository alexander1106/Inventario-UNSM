using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;

namespace Proyecto_de_practicas.Service
{
    public class UsuariosService : IUsuariosServices
    {
        private readonly IUsuariosRepository usuariosRepository;
        private readonly PasswordHasher<Usuario> passwordHasher;
        private readonly IMapper mapper;
        
        public UsuariosService(IUsuariosRepository usuariosRepository, IMapper mapper)
        {
            this.usuariosRepository = usuariosRepository;
            this.passwordHasher = new PasswordHasher<Usuario>();
            this.mapper = mapper;
        }

        // 📌 Obtener todos los usuarios
        public async Task<List<UsuariosDto>> GetAllAsync()
        {
            var usuarios = await usuariosRepository.GetAllAsync();
            return mapper.Map<List<UsuariosDto>>(usuarios);
        }

        // 📌 Obtener usuario por ID
        public async Task<UsuariosDto?> GetByIdAsync(int id)
        {
            var usuario = await usuariosRepository.GetByIdAsync(id);
            return mapper.Map<UsuariosDto?>(usuario);
        }

        // 📌 Crear usuario con contraseña hasheada
        public async Task<UsuariosDto> AddAsync(UsuariosDto usuarioDto)
        {
            // Validar username único
            var existente = await usuariosRepository.GetByNombreAsync(usuarioDto.Username);
            if (existente != null)
                throw new Exception("Ya existe un usuario con ese nombre");

            var usuario = mapper.Map<Usuario>(usuarioDto);
            usuario.Password = passwordHasher.HashPassword(usuario, usuario.Password);

            var nuevo = await usuariosRepository.AddAsync(usuario);
            return mapper.Map<UsuariosDto>(nuevo);
        }

        // 📌 Actualizar usuario
        public async Task<UsuariosDto> UpdateAsync(UsuariosDto usuarioDto)
        {
            var usuario = mapper.Map<Usuario>(usuarioDto);
            var existente = await usuariosRepository.GetByIdAsync(usuario.Id);

            if (existente == null)
                throw new Exception("Usuario no encontrado");

            // Si la contraseña cambió → rehashear
            if (!string.IsNullOrEmpty(usuario.Password) && usuario.Password != existente.Password)
                usuario.Password = passwordHasher.HashPassword(usuario, usuario.Password);
            else
                usuario.Password = existente.Password;

            var actualizado = await usuariosRepository.UpdateAsync(usuario);
            return mapper.Map<UsuariosDto>(actualizado);
        }

        // 📌 Obtener usuario por nombre de usuario
        public async Task<UsuariosDto?> GetByNombreAsync(string nombre)
        {
            var usuario = await usuariosRepository.GetByNombreAsync(nombre);
            return mapper.Map<UsuariosDto?>(usuario);
        }

        // 📌 Eliminar usuario
        public async Task<bool> DeleteAsync(int id)
        {
            return await usuariosRepository.DeleteAsync(id);
        }



        // 📌 Validar login
        public async Task<bool> ValidateLoginAsync(string username, string passwordIngresado)
        {
            var usuario = await usuariosRepository.GetByNombreAsync(username);
            if (usuario == null) return false;

            var result = passwordHasher.VerifyHashedPassword(usuario, usuario.Password, passwordIngresado);
            return result == PasswordVerificationResult.Success;
        }
    }
}
