using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface IUsuariosServices
    {
        Task<List<UsuariosDto>> GetAllAsync();
        Task<UsuariosDto?> GetByIdAsync(int id);
        Task<UsuariosDto> AddAsync(UsuarioCreateDTO usuarioDto);
        Task<UsuariosDto> UpdateAsync(UsuariosDto usuarioDto);
        Task<bool> DeleteAsync(int id);

        Task<bool> ValidateLoginAsync(string username, string passwordIngresado);
        Task<UsuariosDto?> GetByUsernameAsync(string usernameActual);
        Task<UsuariosDto?> GetUsuarioActualAsync(string username);

        // Métodos para clave
        bool VerificarPassword(Usuario usuario, string passwordPlano);
        Task<bool> CambiarPasswordAsync(int idUsuario, string passwordNueva);

        // NUEVO ✔
        Task<Usuario?> GetEntityByUsernameAsync(string usernameActual);

        Task<bool> ActualizarImagenAsync(int usuarioId, IFormFile imagen);


    }
}