using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.Entities;

namespace Proyecto_de_practicas.Repository
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly AplicationDBContext _context;

        public ArticuloRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Articulo>> GetAllAsync()
        {
            return await _context.Articulos
                .Include(a => a.TipoArticulo)
                .Include(a => a.Ubicacion)
                .ToListAsync();
        }

        public async Task<Articulo?> GetByIdAsync(int id)
        {
            return await _context.Articulos
                .Include(a => a.TipoArticulo)
                .Include(a => a.Ubicacion)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Articulo> AddAsync(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        public async Task<Articulo> UpdateAsync(Articulo articulo)
        {
            _context.Articulos.Update(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Articulos.FindAsync(id);
            if (entity == null) return false;

            _context.Articulos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Articulo>> GetByTipoArticuloIdAsync(int tipoArticuloId)
        {
            return await _context.Articulos
                .Where(a => a.TipoArticuloId == tipoArticuloId)
                .Include(a => a.Ubicacion)
                .ToListAsync();
        }

        public async Task<List<Articulo>> GetByUbicacionIdAsync(int ubicacionId)
        {
            return await _context.Articulos
                .Where(a => a.UbicacionId == ubicacionId)
                .Include(a => a.TipoArticulo)
                .ToListAsync();
        }
    }
}
