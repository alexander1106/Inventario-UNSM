using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;

namespace Proyecto_de_practicas.Modules.Security.Repositories
{
    public class SubModulosRepository: ISubModulosRepository
    {
        private readonly AplicationDBContext _context;

        public SubModulosRepository(AplicationDBContext context)
        {
            _context = context;
        }

        // Obtener todos los submódulos
        public async Task<IEnumerable<SubModulo>> GetAllSubModulosAsync()
        {
            return await _context.SubModulos
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener submódulos por ID del módulo padre
        public async Task<IEnumerable<SubModulo>> GetSubModulosByModuloIdAsync(int moduloId)
        {
            return await _context.SubModulos
                .Where(sm => sm.ModuloId == moduloId)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener un submódulo por ID
        public async Task<SubModulo?> GetSubModuloByIdAsync(int id)
        {
            return await _context.SubModulos
                .AsNoTracking()
                .FirstOrDefaultAsync(sm => sm.Id == id);
        }

        // Buscar submódulos por nombre parcial
        public async Task<IEnumerable<SubModulo>> SearchSubModulosByNombreAsync(string nombre)
        {
            return await _context.SubModulos
                .Where(sm => sm.Nombre.Contains(nombre))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}