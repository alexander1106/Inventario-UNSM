using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Repository
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
            return await _context.TipoUbicacion
                .Include(t => t.Ubicaciones)
                    .ThenInclude(u => u.Articulos)
                        .ThenInclude(a => a.TipoArticulo)
                            .ThenInclude(ta => ta.Campos)
                .ToListAsync();
        }


        public async Task<TipoUbicacion?> GetByIdAsync(int id)
        {
            return await _context.TipoUbicacion
                .Include(t => t.Ubicaciones)
                    .ThenInclude(u => u.Articulos)
                        .ThenInclude(a => a.TipoArticulo)
                            .ThenInclude(ta => ta.Campos)
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
            var existing = await _context.TipoUbicacion.FirstOrDefaultAsync(t => t.Id == tipoubicacion.Id);
            if (existing == null)
                throw new InvalidOperationException("Tipo de ubicación no encontrado.");

            // Actualiza manualmente los campos editables
            existing.Nombre = tipoubicacion.Nombre;

            await _context.SaveChangesAsync();
            return existing;
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
