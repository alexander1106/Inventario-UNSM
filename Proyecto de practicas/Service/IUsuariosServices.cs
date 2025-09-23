using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface IUsuariosServices
    {
        Task<List<UsuariosDto>> GetAllAsync();
        Task<UsuariosDto?> GetByIdAsync(int id);
        Task<UsuariosDto?> GetByNombreAsync(string nombre);
        Task<UsuariosDto> AddAsync(UsuariosDto usuario);
        Task<UsuariosDto> UpdateAsync(UsuariosDto usuario); 
        Task<bool> DeleteAsync(int id);
        Task<UsuariosDto?> GetByUsernameAsync(string username);

        Task<bool> ValidateLoginAsync(string username, string passwordIngresado);

    }
}
