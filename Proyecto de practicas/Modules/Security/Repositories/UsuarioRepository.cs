using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Security.DTO;
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
        public async Task<(List<Usuario>, int total)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Rol);

            var total = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, total);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

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
            var actual = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuario.Id);

            if (actual == null)
                throw new Exception("El usuario no existe.");

            if (string.IsNullOrEmpty(usuario.ImagenPath))
                usuario.ImagenPath = actual.ImagenPath;

            if (string.IsNullOrEmpty(usuario.Password))
                usuario.Password = actual.Password;

            if (string.IsNullOrEmpty(usuario.Username))
                usuario.Username = actual.Username;

            if (usuario.Estado == default)
                usuario.Estado = actual.Estado;

            _context.Entry(actual).CurrentValues.SetValues(usuario);

            await _context.SaveChangesAsync();
            return actual;
        }

        public async Task UpdatePasswordAsync(int idUsuario, string passwordHash)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);

            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            usuario.Password = passwordHash;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> FiltrarAsync(UsuarioFiltro filtro)
        {
            var query = _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Rol)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtro.Nombre))
                query = query.Where(u => u.Nombre.Contains(filtro.Nombre));

            if (!string.IsNullOrEmpty(filtro.Username))
                query = query.Where(u => u.Username.Contains(filtro.Username));

            return await query.ToListAsync();
        }
        public async Task<bool> UpdateImagenAsync(int idUsuario, string imagenPath)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null) return false;

            usuario.ImagenPath = imagenPath;

            // Guardamos solo el cambio de imagen
            _context.Entry(usuario).Property(u => u.ImagenPath).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }

}
