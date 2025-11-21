using Proyecto_de_practicas.Modules.Security.DTO;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface IUsuariosServices
    {
        // Obtener todos los usuarios
        Task<List<UsuariosDto>> GetAllAsync();

        // Obtener usuario por ID
        Task<UsuariosDto?> GetByIdAsync(int id);

        // Crear un usuario
        Task<UsuariosDto> AddAsync(UsuarioCreateDTO usuarioDto);

        // Actualizar un usuario
        Task<UsuariosDto> UpdateAsync(UsuariosDto usuarioDto);

        // Eliminar un usuario
        Task<bool> DeleteAsync(int id);

        // Validar login
        Task<bool> ValidateLoginAsync(string username, string passwordIngresado);
        Task<UsuariosDto?> GetByUsernameAsync(string usernameActual);

        Task<UsuariosDto?> GetUsuarioActualAsync(string username);
    }
}
