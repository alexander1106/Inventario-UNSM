using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface IUsuariosServices
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByNombreAsync(string nombre);
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
        Task<bool> ValidateLoginAsync(string username, string passwordIngresado);

    }
}
