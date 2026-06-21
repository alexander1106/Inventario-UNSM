using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Repository
{
    public class SedesRepository : ISedesRepository
    {
        private readonly AplicationDBContext _context;

        public SedesRepository(AplicationDBContext context)
        {
            _context = context;
        }

        // GET NORMAL
        public async Task<IEnumerable<Sedes>> GetAllAsync()
        {
            return await _context.Sedes.ToListAsync();
        }

        // GET DETALLE
        public async Task<IEnumerable<SedeDetalleDto>> GetAllDetalleAsync()
        {
            return await _context.Sedes
                .Select(s => new SedeDetalleDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Direccion = s.Direccion,
                    Estado = s.Estado,
                    NroFacultades = s.Facultades.Count(),
                    NroBienes = 0 // luego lo calculas
                })
                .ToListAsync();
        }

        // GET NORMAL POR ID
        public async Task<Sedes?> GetByIdAsync(int id)
        {
            return await _context.Sedes.FindAsync(id);
        }

        // GET DETALLE POR ID
        public async Task<SedeDetalleDto?> GetDetalleByIdAsync(int id)
        {
            return await _context.Sedes
                .Where(s => s.Id == id)
                .Select(s => new SedeDetalleDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Direccion = s.Direccion,
                    Estado = s.Estado,
                    NroFacultades = s.Facultades.Count(),
                    NroBienes = 0
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Sedes> CreateAsync(Sedes sede)
        {
            _context.Sedes.Add(sede);
            await _context.SaveChangesAsync();
            return sede;
        }

        public async Task UpdateAsync(Sedes sede)
        {
            _context.Sedes.Update(sede);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sede = await _context.Sedes.FindAsync(id);

            if (sede != null)
            {
                _context.Sedes.Remove(sede);
                await _context.SaveChangesAsync();
            }
        }
    }
}