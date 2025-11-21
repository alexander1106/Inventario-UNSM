using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;

namespace Proyecto_de_practicas.Modules.Security.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly AplicationDBContext _context;

        public RolesRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Roles> AddAsync(Roles rol)
        {
            await _context.Roles.AddAsync(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
                return false;

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Roles>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Roles?> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Roles?> GetByNombreAsync(string nombre)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Nombre.ToLower() == nombre.ToLower());
        }

        public async Task<Roles> UpdateAsync(Roles rol)
        {
            var existente = await _context.Roles.FindAsync(rol.Id);

            if (existente == null)
                return null;

            existente.Nombre = rol.Nombre;

            _context.Roles.Update(existente);
            await _context.SaveChangesAsync();

            return existente;
        }
    }
}
