using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data; // Aquí va tu DbContext
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository.IRepository;

namespace Proyecto_de_practicas.Repository
{
    public class TipoArticuloRepository : ITipoArticuloRepository
    {
        private readonly AplicationDBContext _context;

        public TipoArticuloRepository(AplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<TipoArticulo>> GetAllAsync()
        {
            return await _context.TipoArticulos
                .Where(t => t.Estado == 1) // Solo registros con estado 1
                .Include(t => t.Campos)
                .ToListAsync();
        }

        public async Task<TipoArticulo?> GetByIdAsync(int id)
        {
            return await _context.TipoArticulos
                .Include(t => t.Campos)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TipoArticulo> AddAsync(TipoArticulo tipoArticulo)
        {
            _context.TipoArticulos.Add(tipoArticulo);
            await _context.SaveChangesAsync();
            return tipoArticulo;
        }
        public async Task<bool> TieneRelacionConArticulosAsync(int id)
        {
            return await _context.Articulos.AnyAsync(a => a.TipoArticuloId == id);
        }
        public async Task<TipoArticulo> UpdateAsync(TipoArticulo tipoArticulo)
        {
            _context.TipoArticulos.Update(tipoArticulo);
            await _context.SaveChangesAsync();
            return tipoArticulo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TipoArticulos.FindAsync(id);
            if (entity == null)
                return false;

            // 🔹 Marcar como inactivo en lugar de eliminar
            entity.Estado = 0;

            _context.TipoArticulos.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TipoArticulo?> GetByIdWithArticulosAsync(int id)
        {
            return await _context.TipoArticulos
                .Include(t => t.Articulos) // <-- Incluimos la relación
                .FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}