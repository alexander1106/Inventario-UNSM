using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Repository.IRepository;

namespace Proyecto_de_practicas.Repository
{
    public class CampoArticuloRepository : ICampoArticuloRepository
    {
        private readonly AplicationDBContext _context;

        public CampoArticuloRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<CampoArticulo>> GetAllAsync()
        {
            return await _context.CamposArticulos.ToListAsync();
        }

        public async Task<CampoArticulo?> GetByIdAsync(int id)
        {
            return await _context.CamposArticulos.FindAsync(id);
        }

        public async Task<CampoArticulo> AddAsync(CampoArticulo campoArticulo)
        {
            _context.CamposArticulos.Add(campoArticulo);
            await _context.SaveChangesAsync();
            return campoArticulo;
        }

        public async Task<CampoArticulo> UpdateAsync(CampoArticulo campoArticulo)
        {
            _context.CamposArticulos.Update(campoArticulo);
            await _context.SaveChangesAsync();
            return campoArticulo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CamposArticulos.FindAsync(id);
            if (entity == null) return false;

            _context.CamposArticulos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CampoArticulo>> GetByTipoArticuloIdAsync(int tipoArticuloId)
        {
            return await _context.CamposArticulos
                .Where(c => c.TipoArticuloId == tipoArticuloId)
                .ToListAsync();
        }

        public async Task<bool> ExistsDuplicateAsync(string nombreCampo, int tipoArticuloId, int? excludeId = null)
        {
            return await _context.CamposArticulos
                .AnyAsync(c =>
                    c.NombreCampo.ToLower() == nombreCampo.ToLower() &&
                    c.TipoArticuloId == tipoArticuloId &&
                    (!excludeId.HasValue || c.Id != excludeId.Value));
        }

        public async Task<bool> HasRelationsAsync(int campoArticuloId)
        {
            return await _context.ArticuloCamposValores
                .AnyAsync(v => v.CampoArticuloId == campoArticuloId);
        }


    }
}