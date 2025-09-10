using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Data;

namespace Proyecto_de_practicas.Repository
{

    public class LaboratoriosRepository : ILaboratoriosRepository
    {
        private readonly AplicationDBContext _context;

        public LaboratoriosRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Laboratorios>> GetAllAsync()
        {
            return await _context.Laboratorios.ToListAsync();
        }

        public async Task<Laboratorios?> GetByIdAsync(int id)
        {
            return await _context.Laboratorios.FindAsync(id);
        }

        public async Task<Laboratorios?> GetByNombreAsync(string nombre)
        {
            return await _context.Laboratorios.FirstOrDefaultAsync(l => l.Nombre == nombre);
        }

        public async Task<Laboratorios> AddAsync(Laboratorios laboratorio)
        {
            _context.Laboratorios.Add(laboratorio);
            await _context.SaveChangesAsync();
            return laboratorio;
        }

        public async Task<Laboratorios> UpdateAsync(Laboratorios laboratorio)
        {
            _context.Laboratorios.Update(laboratorio);
            await _context.SaveChangesAsync();
            return laboratorio;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lab = await _context.Laboratorios.FindAsync(id);
            if (lab == null) return false;

            _context.Laboratorios.Remove(lab);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}