using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public class UsuarioRepository : IUsuariosRepository
    {
        private readonly AplicationDBContext _context;
        public UsuarioRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return false;
            user.EstadoInt = 0;
            _context.Usuarios.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await  _context.Usuarios.Where(u=>u.EstadoInt == 1).ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id && u.EstadoInt==1 );

        }

        public async Task<Usuario?> GetByNombreAsync(string nombre)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombre && u.EstadoInt ==1);

        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
