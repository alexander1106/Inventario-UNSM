using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;

namespace Proyecto_de_practicas.Modules.Security.Repositories
{
    public class UsuarioRepository : IUsuariosRepository
    {
        private readonly AplicationDBContext _context;

        public UsuarioRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(x => x.Rol)
                .ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(x => x.Rol)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(x => x.Rol)
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            var exists = await _context.Usuarios.AnyAsync(x => x.Id == usuario.Id);
            if (!exists)
            {
                throw new Exception("El usuario no existe.");
            }

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdatePasswordAsync(int idUsuario, string passwordHash)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);

            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            usuario.Password = passwordHash;

            await _context.SaveChangesAsync();
        }
    }
}
