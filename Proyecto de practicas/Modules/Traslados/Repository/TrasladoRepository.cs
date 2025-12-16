using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Traslados.Entities;
using Proyecto_de_practicas.Modules.Traslados.Repository.IRespository;

namespace Proyecto_de_practicas.Modules.Traslados.Repository
{
    public class TrasladoRepository : ITrasladoRepository
    {
        private readonly AplicationDBContext _context;
        private readonly DbSet<Traslado> _dbSet;

        public TrasladoRepository(AplicationDBContext context)
        {
            _context = context;
            _dbSet = context.Set<Traslado>();
        }

        // 🔹 MÉTODO CLAVE IMPLEMENTADO
        public async Task<bool> RealizarTrasladoAsync(Traslado traslado)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Obtener artículo
                var articulo = await _context.Set<Articulo>()
                    .FirstOrDefaultAsync(a => a.Id == traslado.ArticuloId);

                if (articulo == null)
                    return false;

                // 2️⃣ Validar ubicación origen
                if (articulo.UbicacionId != traslado.UbicacionOrigenId)
                    throw new Exception("El artículo no se encuentra en la ubicación origen");

                // 3️⃣ Mover artículo
                articulo.UbicacionId = traslado.UbicacionDestinoId;

                // 4️⃣ Guardar traslado
                await _dbSet.AddAsync(traslado);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // ---------------- CRUD NORMAL ----------------

        public async Task<List<Traslado>> GetAllAsync()
        {
            return await _dbSet
                .Include(t => t.Articulo)
                .Include(t => t.UbicacionOrigen)
                .Include(t => t.UbicacionDestino)
                .Include(t => t.Usuario)
                .ToListAsync();
        }

        public async Task<Traslado?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Articulo)
                .Include(t => t.UbicacionOrigen)
                .Include(t => t.UbicacionDestino)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Traslado> CreateAsync(Traslado traslado)
        {
            await _dbSet.AddAsync(traslado);
            await _context.SaveChangesAsync();
            return traslado;
        }

        public async Task<Traslado> UpdateAsync(Traslado traslado)
        {
            _dbSet.Update(traslado);
            await _context.SaveChangesAsync();
            return traslado;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var traslado = await _dbSet.FindAsync(id);
            if (traslado == null) return false;

            _dbSet.Remove(traslado);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
