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
            var lab = await _context.Usuarios.FindAsync(id);
            if (lab == null) return false;

            _context.Usuarios.Remove(lab);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await  _context.Usuarios.ToListAsync();

        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        }

        public async Task<Usuario?> GetByNombreAsync(string nombre)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombre);

        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
