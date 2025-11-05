using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository.IRepository;

namespace Proyecto_de_practicas.Repository
{
    public class UsuarioRepository : IUsuariosRepository
    {
        private readonly AplicationDBContext _context;
        public UsuarioRepository(AplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();

        }

        public async Task<Usuario?> GetByIdAsync(int id)
        { 
           return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Ubicaciones.FindAsync(id);
            if (entity == null) return false;

            _context.Ubicaciones.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> GetByNombreAsync(string username)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == username);
        }

       
    }
}
