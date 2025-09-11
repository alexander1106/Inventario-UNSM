using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Data;

namespace Proyecto_de_practicas.Repository
{
    public class EquiposRepository : IEquiposRepository
    {
        private readonly AplicationDBContext _context;

        public EquiposRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Equipos>> GetAllAsync()
        {
            return await _context.Equipos.ToListAsync();
        }
        public async Task<Equipos?> GetByIdAsync(int id)
        {
            return await _context.Equipos.FindAsync(id);
        }
        public async Task<Equipos?> GetByNombreAsync(string nombre)
        {
            return await _context.Equipos.FirstOrDefaultAsync(l => l.Nombre == nombre);
        }
        public async Task<Equipos> AddAsync(Equipos equipo)
        {
            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();
            return equipo;
        }
        public async Task<Equipos> UpdateAsync(Equipos equipo)
        {
            _context.Equipos.Update(equipo);
            await _context.SaveChangesAsync();
            return equipo;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null) return false;

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
