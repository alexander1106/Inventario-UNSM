using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Data;

namespace Proyecto_de_practicas.Repository
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly AplicationDBContext _context;

        public CategoriasRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Categorias>> GetAllAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categorias?> GetByIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task<Categorias?> GetByNombreAsync(string nombre)
        {
            return await _context.Categorias.FirstOrDefaultAsync(l => l.Nombre == nombre);
        }

        public async Task<Categorias> AddAsync(Categorias categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categorias> UpdateAsync(Categorias categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cat = await _context.Categorias.FindAsync(id);
            if (cat == null) return false;

            _context.Categorias.Remove(cat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
