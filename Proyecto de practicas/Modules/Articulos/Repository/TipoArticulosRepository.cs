using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;

namespace Proyecto_de_practicas.Modules.Articulos.Repository
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
                .Where(t => t.Estado == 1)
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

            entity.Estado = 0;
            _context.TipoArticulos.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TieneRelacionConArticulosAsync(int id)
        {
            return await _context.Articulos.AnyAsync(a => a.TipoArticuloId == id);
        }

        public async Task<TipoArticulo?> GetByIdWithArticulosAsync(int id)
        {
            return await _context.TipoArticulos
                .Include(t => t.Articulos)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // Nuevo método para SP: devuelve solo List<string>
        public async Task<List<string>> GetEncabezadoArticulosAsync(int idTipoArticulo)
        {
            var resultados = await _context.Set<EncabezadoResult>()
                .FromSqlRaw("EXEC sp_GetEncabezadoArticulos @idTipoArticulo = {0}", idTipoArticulo)
                .ToListAsync();

            return resultados.Select(r => r.Encabezado).ToList();
        }
        public async Task<bool> ExisteNombreAsync(string nombre, int? excluirId = null)
        {
            var query = _context.TipoArticulos
                .Where(t => t.Nombre.ToLower() == nombre.ToLower());

            if (excluirId.HasValue)
                query = query.Where(t => t.Id != excluirId.Value);

            return await query.AnyAsync();
        }


    }
}
