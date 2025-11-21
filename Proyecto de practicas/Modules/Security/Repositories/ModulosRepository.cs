using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;

namespace Proyecto_de_practicas.Modules.Security.Repositories
{
    public class ModulosRepository : IModulosRepository
    {
        private readonly AplicationDBContext _context;

        public ModulosRepository(AplicationDBContext context)
        {
            _context = context;
        }

        // Obtener todos los módulos
        public async Task<IEnumerable<Modulo>> GetAllModulosAsync()
        {
            return await _context.Modulos
                 .Include(m => m.SubModulos) // 👈 NECESARIO PARA CARGAR SUBMÓDULOS
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener módulo por ID
        public async Task<Modulo?> GetModuloByIdAsync(int id)
        {
            return await _context.Modulos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Buscar módulos por nombre parcial
        public async Task<IEnumerable<Modulo>> SearchModulosByNombreAsync(string nombre)
        {
            return await _context.Modulos
                .Where(m => m.Nombre.Contains(nombre))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}