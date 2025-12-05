using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Repositories.IRepositories
{
    public interface IUsuariosRepository
    {
        Task<List<Usuario>> GetAllAsync();          
        Task<Usuario?> GetByIdAsync(int id);          
        Task<Usuario> UpdateAsync(Usuario usuario);  
        Task<bool> DeleteAsync(int id);
        Task<Usuario?> GetByUsernameAsync(string username);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task UpdatePasswordAsync(int idUsuario, string passwordHash);


    }
}