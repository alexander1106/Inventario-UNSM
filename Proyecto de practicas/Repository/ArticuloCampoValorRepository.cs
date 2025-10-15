using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
   public class ArticuloCampoValorRepository : IArticuloCampoValorRepository
    {
        private readonly AplicationDBContext _context;

        public ArticuloCampoValorRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArticuloCampoValor>> GetAllAsync()
        {
            return await _context.ArticuloCamposValores.ToListAsync();
        }

        public async Task<ArticuloCampoValor?> GetByIdAsync(int id)
        {
            return await _context.ArticuloCamposValores.FindAsync(id);
        }
        public async Task<IEnumerable<ArticuloCampoValor>> GetByTipoArticuloIdAsync(int tipoArticuloId)
        {
            return await _context.ArticuloCamposValores
                .Include(acv => acv.CampoArticulo) // incluir la relación
                .Where(acv => acv.CampoArticulo.TipoArticuloId == tipoArticuloId)
                .ToListAsync();
        }


        public async Task AddAsync(ArticuloCampoValor entity)
        {
            await _context.ArticuloCamposValores.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ArticuloCampoValor entity)
        {
            _context.ArticuloCamposValores.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ArticuloCamposValores.FindAsync(id);
            if (entity != null)
            {
                _context.ArticuloCamposValores.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<ArticuloCampoValor>> GetByArticuloIdAsync(int articuloId)
        {
            throw new NotImplementedException();
        }
    }
}