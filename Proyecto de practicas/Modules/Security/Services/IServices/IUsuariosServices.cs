using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Modules.Security.DTO;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface IUsuariosServices
    {
        Task<List<UsuariosDto>> GetAllAsync();
        Task<UsuariosDto?> GetByIdAsync(int id);
        Task<UsuariosDto?> GetByNombreAsync(string nombre);
        Task<UsuariosDto?> GetByUsernameAsync(string username);

        Task<UsuariosDto> AddAsync(UsuariosDto usuario);
        Task<UsuariosDto> UpdateAsync(UsuariosDto usuario);
        Task<bool> DeleteAsync(int id);

        Task<bool> ValidateLoginAsync(string username, string passwordIngresado);

    }
}
