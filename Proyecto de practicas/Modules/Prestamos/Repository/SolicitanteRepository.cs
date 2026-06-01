using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;

namespace Proyecto_de_practicas.Modules.Prestamos.Repository
{
    public class SolicitanteRepository : ISolicitanteRepository
    {
        private readonly AplicationDBContext _context;

        public SolicitanteRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Solicitantes>> GetAllAsync()
        {
            return await _context.Set<Solicitantes>()
                .Include(x => x.Ubicacion)
                .ToListAsync();
        }

        public async Task<Solicitantes?> GetByIdAsync(int id)
        {
            return await _context.Set<Solicitantes>()
                .Include(x => x.Ubicacion)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Solicitantes> AddAsync(Solicitantes solicitante)
        {
            _context.Set<Solicitantes>().Add(solicitante);
            await _context.SaveChangesAsync();
            return solicitante;
        }

        public async Task UpdateAsync(Solicitantes solicitante)
        {
            _context.Set<Solicitantes>().Update(solicitante);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.Set<Solicitantes>()
                .AnyAsync(x => x.Codigo == codigo);
        }
        public async Task DeleteAsync(Solicitantes solicitante)
        {
            _context.Set<Solicitantes>().Remove(solicitante);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Solicitantes>> GetByUsuarioAsync(int usuarioId)
        {
            return await _context.Set<Solicitantes>()
                .Include(x => x.Ubicacion)
                .Where(x => x.Ubicacion.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}
