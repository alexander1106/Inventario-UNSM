using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Data;

namespace Proyecto_de_practicas.Repository
{
    public class FacultadesRepository : IFacultadesRepository
    {
        private readonly AplicationDBContext _context;

        public FacultadesRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Facultades>> GetAllAsync()
        {
            return await _context.Facultades.ToListAsync();
        }

        public async Task<Facultades?> GetByIdAsync(int id)
        {
            return await _context.Facultades.FindAsync(id);
        }

        public async Task<Facultades?> GetByNombreAsync(string nombre)
        {
            return await _context.Facultades.FirstOrDefaultAsync(l => l.Nombre == nombre);
        }

        public async Task<Facultades> AddAsync(Facultades facultad)
        {
            _context.Facultades.Add(facultad);
            await _context.SaveChangesAsync();
            return facultad;
        }

        public async Task<Facultades> UpdateAsync(Facultades facultad)
        {
            _context.Facultades.Update(facultad);
            await _context.SaveChangesAsync();
            return facultad;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fac = await _context.Facultades.FindAsync(id);
            if (fac == null) return false;

            _context.Facultades.Remove(fac);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
