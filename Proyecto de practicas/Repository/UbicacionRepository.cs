using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public class UbicacionRepository : IUbicacionRepository
    {
        private readonly AplicationDBContext _context;

        public UbicacionRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Ubicacion>> GetAllAsync()
        {
            return await _context.Ubicaciones
                .Include(u => u.TipoUbicacion)
                .Include(u => u.Articulos)
                .ToListAsync();
        }

        public async Task<Ubicacion?> GetByIdAsync(int id)
        {
            return await _context.Ubicaciones
                .Include(u => u.TipoUbicacion)
                .Include(u => u.Articulos)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Ubicacion> AddAsync(Ubicacion ubicacion)
        {
            _context.Ubicaciones.Add(ubicacion);
            await _context.SaveChangesAsync();
            return ubicacion;
        }

        public async Task<Ubicacion> UpdateAsync(Ubicacion ubicacion)
        {
            _context.Ubicaciones.Update(ubicacion);
            await _context.SaveChangesAsync();
            return ubicacion;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Ubicaciones.FindAsync(id);
            if (entity == null) return false;

            _context.Ubicaciones.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
