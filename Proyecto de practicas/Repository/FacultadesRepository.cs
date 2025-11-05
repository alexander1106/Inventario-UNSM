using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Repository.IRepository;

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
            return await _context.Facultades.Where(f => f.Estado == 1).ToListAsync();
        }

        public async Task<Facultades?> GetByIdAsync(int id)
        {
            return await _context.Facultades.FirstOrDefaultAsync(f => f.Id == id && f.Estado == 1); // 👈 filtrar activas
            ;
        }

        public async Task<Facultades?> GetByNombreAsync(string nombre)
        {
            return await _context.Facultades
                            .FirstOrDefaultAsync(f => f.Nombre == nombre && f.Estado == 1);
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
            var facultad = await _context.Facultades.FindAsync(id);
            if (facultad == null) return false;

            // 👇 aquí el borrado lógico
            facultad.Estado = 0;
            _context.Facultades.Update(facultad);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}


