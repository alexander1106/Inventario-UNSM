using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface IUsuariosServices
    {
        Task<List<UsuarioResponseDTO>> GetAllAsync();
        Task<UsuarioResponseDTO?> GetByIdAsync(int id);
        Task<UsuarioResponseDTO> AddAsync(UsuarioCreateDTO usuarioDto);
        Task<UsuarioResponseDTO?> UpdateAsync(UsuarioUpdateDTO usuarioDto);
        Task<bool> DeleteAsync(int id);

        Task<bool> ValidateLoginAsync(string username, string passwordIngresado);
        Task<UsuarioResponseDTO?> GetByUsernameAsync(string usernameActual);

        bool VerificarPassword(Usuario usuario, string passwordPlano);
        Task<bool> CambiarPasswordAsync(int idUsuario, string passwordNueva);

        Task<Usuario?> GetEntityByUsernameAsync(string usernameActual);
        Task<bool> ActualizarImagenAsync(int usuarioId, IFormFile imagen);
        Task<List<UsuarioResponseDTO>> FiltrarAsync(UsuarioFiltro filtro);
        Task<(List<UsuarioResponseDTO>, int)> GetPagedAsync(int page, int pageSize);
    }
}