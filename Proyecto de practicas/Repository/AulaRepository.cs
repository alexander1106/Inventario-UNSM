using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public class AulaRepository : IAulasRepository
    {
        private readonly AplicationDBContext _context;
        public AulaRepository(AplicationDBContext context)
        {
            _context = context;
        }


        public async Task<Aulas> AddAsync(Aulas aulas)
        {
            _context.Aulas.Add(aulas);
            await _context.SaveChangesAsync();
            return aulas;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lab = await _context.Laboratorios.FindAsync(id);
            if (lab == null) return false;

            _context.Laboratorios.Remove(lab);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Aulas>> GetAllAsync()
        {
            return await _context.Aulas.ToListAsync();
        }

        public async Task<Aulas?> GetByIdAsync(int id)
        {
            return await _context.Aulas.FindAsync(id);
        }

        public async Task<Aulas?> GetByNombreAsync(string nombre)
        {
            return await _context.Aulas.FirstOrDefaultAsync(l => l.Nombre == nombre);
        }

        public async Task<Aulas> UpdateAsync(Aulas aulas)
        {
            _context.Aulas.Update(aulas);
            await _context.SaveChangesAsync();
            return aulas;
        }
    }
}
