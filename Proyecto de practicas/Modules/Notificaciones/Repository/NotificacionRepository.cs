using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Notificaciones.Entities;

namespace Proyecto_de_practicas.Modules.Notificaciones.Repository
{
    public class NotificacionRepository : INotificacionRepository
    {
        private readonly AplicationDBContext _context;

        public NotificacionRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Notificacion>> GetAllAsync(int usuarioDestinoId, bool? soloNoLeidas)
        {
            var query = _context.Notificaciones
                .Where(n => n.UsuarioDestinoId == usuarioDestinoId);

            if (soloNoLeidas == true)
                query = query.Where(n => !n.Leido);

            return await query
                .OrderByDescending(n => n.FechaCreacion)
                .ToListAsync();
        }

        public async Task<int> CountNoLeidasAsync(int usuarioDestinoId)
        {
            return await _context.Notificaciones
                .CountAsync(n => n.UsuarioDestinoId == usuarioDestinoId && !n.Leido);
        }

        public async Task<Notificacion?> GetByIdAsync(int id)
        {
            return await _context.Notificaciones.FindAsync(id);
        }

        public async Task<Notificacion> AddAsync(Notificacion entity)
        {
            _context.Notificaciones.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ExisteNotificacionActivaAsync(string tipo, int? articuloId, int? prestamoId, int usuarioDestinoId, int? trasladoId = null)
        {
            return await _context.Notificaciones.AnyAsync(n =>
                n.Tipo == tipo &&
                !n.Leido &&
                n.ArticuloId == articuloId &&
                n.PrestamoId == prestamoId &&
                n.TrasladoId == trasladoId &&
                n.UsuarioDestinoId == usuarioDestinoId);
        }

        public async Task<Notificacion?> MarcarLeidaAsync(int id, int usuarioDestinoId)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null || notificacion.UsuarioDestinoId != usuarioDestinoId)
                return null;

            notificacion.Leido = true;
            notificacion.FechaLectura = DateTime.Now;
            await _context.SaveChangesAsync();

            return notificacion;
        }

        public async Task MarcarTodasLeidasAsync(int usuarioDestinoId)
        {
            var pendientes = await _context.Notificaciones
                .Where(n => !n.Leido && n.UsuarioDestinoId == usuarioDestinoId)
                .ToListAsync();

            var ahora = DateTime.Now;
            foreach (var notificacion in pendientes)
            {
                notificacion.Leido = true;
                notificacion.FechaLectura = ahora;
            }

            await _context.SaveChangesAsync();
        }

        public async Task MarcarLeidasPorPrestamoAsync(int prestamoId)
        {
            var pendientes = await _context.Notificaciones
                .Where(n => !n.Leido && n.PrestamoId == prestamoId && n.Tipo == NotificacionTipos.PrestamoPendiente)
                .ToListAsync();

            var ahora = DateTime.Now;
            foreach (var notificacion in pendientes)
            {
                notificacion.Leido = true;
                notificacion.FechaLectura = ahora;
            }

            await _context.SaveChangesAsync();
        }

        public async Task MarcarLeidasPorTrasladoAsync(int trasladoId)
        {
            var pendientes = await _context.Notificaciones
                .Where(n => !n.Leido && n.TrasladoId == trasladoId && n.Tipo == NotificacionTipos.TrasladoRegistrado)
                .ToListAsync();

            var ahora = DateTime.Now;
            foreach (var notificacion in pendientes)
            {
                notificacion.Leido = true;
                notificacion.FechaLectura = ahora;
            }

            await _context.SaveChangesAsync();
        }
    }
}
