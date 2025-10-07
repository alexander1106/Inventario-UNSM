using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public class TipoUbicacionRepository : ITipoUbicacionRepository
    {
        private readonly AplicationDBContext _context;

        public TipoUbicacionRepository(AplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<TipoUbicacion>> GetAllAsync()
        {
            return await _context.TipoUbicacion.ToListAsync();
        }

        public async Task<TipoUbicacion?> GetByIdAsync(int id)
        {
            return await _context.TipoUbicacion
                                 .Include(t => t.Ubicaciones) 
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TipoUbicacion> AddAsync(TipoUbicacion tipoubicacion)
        {
            _context.TipoUbicacion.Add(tipoubicacion);
            await _context.SaveChangesAsync();
            return tipoubicacion;
        }

        public async Task<TipoUbicacion> UpdateAsync(TipoUbicacion tipoubicacion)
        {
            _context.TipoUbicacion.Update(tipoubicacion);
            await _context.SaveChangesAsync();
            return tipoubicacion;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TipoUbicacion
                                       .Include(t => t.Ubicaciones) 
                                       .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) return false;

            if (entity.Ubicaciones.Any())
                throw new InvalidOperationException("No se puede eliminar un tipo de ubicación que tiene ubicaciones asociadas.");

            _context.TipoUbicacion.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
