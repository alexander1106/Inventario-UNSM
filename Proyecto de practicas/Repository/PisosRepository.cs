using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Data;

namespace Proyecto_de_practicas.Repository
{
    public class PisosRepository : IPisosRepository
    {
        private readonly AplicationDBContext _context;

        public PisosRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Pisos>> GetAllAsync()
        {
            return await _context.Pisos.ToListAsync();
        }

        public async Task<Pisos?> GetByIdAsync(int id)
        {
            return await _context.Pisos.FindAsync(id);
        }

        public async Task<Pisos?> GetByNumeroAsync(int numero)
        {
            return await _context.Pisos.FirstOrDefaultAsync(l => l.Numero == numero);
        }

        public async Task<Pisos> AddAsync(Pisos piso)
        {
            _context.Pisos.Add(piso);
            await _context.SaveChangesAsync();
            return piso;
        }

        public async Task<Pisos> UpdateAsync(Pisos piso)
        {
            _context.Pisos.Update(piso);
            await _context.SaveChangesAsync();
            return piso;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lab = await _context.Pisos.FindAsync(id);
            if (lab == null) return false;

            _context.Pisos.Remove(lab);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
