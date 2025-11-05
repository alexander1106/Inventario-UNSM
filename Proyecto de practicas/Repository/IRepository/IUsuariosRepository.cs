using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository.IRepository
{
    public interface IUsuariosRepository
    {
        Task<List<Usuario>> GetAllAsync();              // 🔹 Ahora devuelve usuarios
        Task<Usuario?> GetByIdAsync(int id);            // 🔹 Usuario
        Task<Usuario> AddAsync(Usuario usuario);        // 🔹 Usuario
        Task<Usuario> UpdateAsync(Usuario usuario);     // 🔹 Usuario
        Task<bool> DeleteAsync(int id);
        Task<Usuario?> GetByNombreAsync(string username);
    }
}