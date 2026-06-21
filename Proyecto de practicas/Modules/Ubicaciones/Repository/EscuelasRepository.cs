using System;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;
using Microsoft.EntityFrameworkCore;
namespace Proyecto_de_practicas.Modules.Ubicaciones.Repository
{
    public class EscuelasRepository : IEscuelasRepository
    {
        private readonly AplicationDBContext _context;

        public EscuelasRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Escuelas>> GetAllAsync()
        {
            return await _context.Escuelas
                .Include(e => e.Facultad)
                .ToListAsync();
        }

        public async Task<Escuelas?> GetByIdAsync(int id)
        {
            return await _context.Escuelas
                .Include(e => e.Facultad)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Escuelas>> GetByFacultadIdAsync(int facultadId)
        {
            return await _context.Escuelas
                .Where(e => e.FacultadId == facultadId)
                .ToListAsync();
        }

        public async Task<Escuelas> CreateAsync(Escuelas escuela)
        {
            _context.Escuelas.Add(escuela);
            await _context.SaveChangesAsync();
            return escuela;
        }

        public async Task UpdateAsync(Escuelas escuela)
        {
            _context.Escuelas.Update(escuela);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var escuela = await _context.Escuelas.FindAsync(id);

            if (escuela != null)
            {
                _context.Escuelas.Remove(escuela);
                await _context.SaveChangesAsync();
            }
        }
    }
}