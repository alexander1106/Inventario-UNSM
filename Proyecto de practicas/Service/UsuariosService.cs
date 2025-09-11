using Microsoft.AspNetCore.Identity;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;

namespace Proyecto_de_practicas.Service
{
    public class UsuariosService : IUsuariosServices
    {
        private readonly IUsuariosRepository usuariosRepository;
        private readonly PasswordHasher<Usuario> passwordHasher;

        public UsuariosService(IUsuariosRepository usuariosRepository)
        {
            this.usuariosRepository = usuariosRepository;
            this.passwordHasher = new PasswordHasher<Usuario>();
        }

        // Agregar usuario con contraseña hasheada
        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            // Validar nombre único
            var existente = await usuariosRepository.GetByNombreAsync(usuario.Username);
            if (existente != null)
                throw new Exception("Ya existe un usuario con ese nombre");

            // Hashear la contraseña
            usuario.Password = passwordHasher.HashPassword(usuario, usuario.Password);

            return await usuariosRepository.AddAsync(usuario);
        }

        // Eliminar usuario por ID
        public async Task<bool> DeleteAsync(int id)
        {
            return await usuariosRepository.DeleteAsync(id);
        }

        // Obtener todos los usuarios
        public async Task<List<Usuario>> GetAllAsync()
        {
            return await usuariosRepository.GetAllAsync();
        }

        // Obtener usuario por ID
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await usuariosRepository.GetByIdAsync(id);
        }

        // Obtener usuario por nombre de usuario
        public async Task<Usuario?> GetByNombreAsync(string nombre)
        {
            return await usuariosRepository.GetByNombreAsync(nombre);
        }

        // Actualizar usuario
        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            var existente = await usuariosRepository.GetByIdAsync(usuario.Id);
            if (existente == null)
                throw new Exception("Usuario no encontrado");

            // Si la contraseña cambió, la re-hasheamos
            if (!string.IsNullOrEmpty(usuario.Password) && usuario.Password != existente.Password)
            {
                usuario.Password = passwordHasher.HashPassword(usuario, usuario.Password);
            }
            else
            {
                // Mantener la contraseña actual si no se cambió
                usuario.Password = existente.Password;
            }

            return await usuariosRepository.UpdateAsync(usuario);
        }

        // Verificar login
        public async Task<bool> ValidateLoginAsync(string username, string passwordIngresado)
        {
            var usuario = await usuariosRepository.GetByNombreAsync(username);
            if (usuario == null)
                return false;

            var result = passwordHasher.VerifyHashedPassword(usuario, usuario.Password, passwordIngresado);
            return result == PasswordVerificationResult.Success;
        }
    }
}
