using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Notificaciones.DTO;
using Proyecto_de_practicas.Modules.Notificaciones.Entities;
using Proyecto_de_practicas.Modules.Notificaciones.Repository;

namespace Proyecto_de_practicas.Modules.Notificaciones.Services
{
    public class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository _repo;
        private readonly AplicationDBContext _context;

        public NotificacionService(INotificacionRepository repo, AplicationDBContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<List<NotificacionDto>> GetAllAsync(string username, bool? soloNoLeidas)
        {
            var usuarioId = await ResolverUsuarioIdAsync(username);
            if (usuarioId == null)
                return new List<NotificacionDto>();

            var entities = await _repo.GetAllAsync(usuarioId.Value, soloNoLeidas);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<int> CountNoLeidasAsync(string username)
        {
            var usuarioId = await ResolverUsuarioIdAsync(username);
            return usuarioId == null ? 0 : await _repo.CountNoLeidasAsync(usuarioId.Value);
        }

        public async Task<NotificacionDto?> MarcarLeidaAsync(int id, string username)
        {
            var usuarioId = await ResolverUsuarioIdAsync(username);
            if (usuarioId == null)
                return null;

            var entity = await _repo.MarcarLeidaAsync(id, usuarioId.Value);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task MarcarTodasLeidasAsync(string username)
        {
            var usuarioId = await ResolverUsuarioIdAsync(username);
            if (usuarioId == null)
                return;

            await _repo.MarcarTodasLeidasAsync(usuarioId.Value);
        }

        public async Task NotificarPrestamoPendienteAsync(int prestamoId, int articuloId, string? nombreSolicitante)
        {
            var aprobadores = await ObtenerUsuariosAprobadoresAsync();

            var mensaje = string.IsNullOrWhiteSpace(nombreSolicitante)
                ? $"El préstamo #{prestamoId} está pendiente de aprobación."
                : $"El préstamo #{prestamoId} solicitado por {nombreSolicitante} está pendiente de aprobación.";

            foreach (var usuarioId in aprobadores)
            {
                var yaExiste = await _repo.ExisteNotificacionActivaAsync(
                    NotificacionTipos.PrestamoPendiente, null, prestamoId, usuarioId);

                if (yaExiste)
                    continue;

                await _repo.AddAsync(new Notificacion
                {
                    Tipo = NotificacionTipos.PrestamoPendiente,
                    Titulo = "Nuevo préstamo por aprobar",
                    Mensaje = mensaje,
                    ArticuloId = articuloId,
                    PrestamoId = prestamoId,
                    UsuarioDestinoId = usuarioId,
                    FechaCreacion = DateTime.Now
                });
            }
        }

        public async Task NotificarVidaUtilProximaAsync(int articuloId, string mensaje)
        {
            var aprobadores = await ObtenerUsuariosAprobadoresAsync();

            foreach (var usuarioId in aprobadores)
            {
                var yaExiste = await _repo.ExisteNotificacionActivaAsync(
                    NotificacionTipos.VidaUtilProxima, articuloId, null, usuarioId);

                if (yaExiste)
                    continue;

                await _repo.AddAsync(new Notificacion
                {
                    Tipo = NotificacionTipos.VidaUtilProxima,
                    Titulo = "Vida útil próxima a vencer",
                    Mensaje = mensaje,
                    ArticuloId = articuloId,
                    UsuarioDestinoId = usuarioId,
                    FechaCreacion = DateTime.Now
                });
            }
        }

        public async Task NotificarPrestamoAprobadoAsync(int prestamoId, int articuloId, int usuarioRegistroId)
        {
            var yaExiste = await _repo.ExisteNotificacionActivaAsync(
                NotificacionTipos.PrestamoAprobado, null, prestamoId, usuarioRegistroId);

            if (yaExiste)
                return;

            await _repo.AddAsync(new Notificacion
            {
                Tipo = NotificacionTipos.PrestamoAprobado,
                Titulo = "Préstamo aprobado",
                Mensaje = $"El préstamo #{prestamoId} que registraste ha sido aprobado.",
                ArticuloId = articuloId,
                PrestamoId = prestamoId,
                UsuarioDestinoId = usuarioRegistroId,
                FechaCreacion = DateTime.Now
            });
        }

        public async Task ResolverNotificacionesPrestamoAsync(int prestamoId)
        {
            await _repo.MarcarLeidasPorPrestamoAsync(prestamoId);
        }

        public async Task NotificarMantenimientoRegistradoAsync(int articuloId, string tipoMantenimiento)
        {
            var aprobadores = await ObtenerUsuariosAprobadoresAsync();

            foreach (var usuarioId in aprobadores)
            {
                var yaExiste = await _repo.ExisteNotificacionActivaAsync(
                    NotificacionTipos.MantenimientoRegistrado, articuloId, null, usuarioId);

                if (yaExiste)
                    continue;

                await _repo.AddAsync(new Notificacion
                {
                    Tipo = NotificacionTipos.MantenimientoRegistrado,
                    Titulo = "Mantenimiento registrado",
                    Mensaje = $"Se registró un mantenimiento ({tipoMantenimiento}) para el artículo #{articuloId}.",
                    ArticuloId = articuloId,
                    UsuarioDestinoId = usuarioId,
                    FechaCreacion = DateTime.Now
                });
            }
        }

        public async Task NotificarMantenimientoCompletadoAsync(int articuloId)
        {
            var aprobadores = await ObtenerUsuariosAprobadoresAsync();
            var descripcionArticulo = await ObtenerDescripcionArticuloAsync(articuloId);

            foreach (var usuarioId in aprobadores)
            {
                var yaExiste = await _repo.ExisteNotificacionActivaAsync(
                    NotificacionTipos.MantenimientoCompletado, articuloId, null, usuarioId);

                if (yaExiste)
                    continue;

                await _repo.AddAsync(new Notificacion
                {
                    Tipo = NotificacionTipos.MantenimientoCompletado,
                    Titulo = "Mantenimiento completado",
                    Mensaje = $"El mantenimiento del artículo {descripcionArticulo} fue completado.",
                    ArticuloId = articuloId,
                    UsuarioDestinoId = usuarioId,
                    FechaCreacion = DateTime.Now
                });
            }
        }

        public async Task NotificarTrasladoRegistradoAsync(int trasladoId, int articuloId, string? nombreUsuario)
        {
            var aprobadores = await ObtenerUsuariosAprobadoresAsync();

            var mensaje = string.IsNullOrWhiteSpace(nombreUsuario)
                ? $"Se registró el traslado #{trasladoId}."
                : $"El traslado #{trasladoId} fue registrado por {nombreUsuario}.";

            foreach (var usuarioId in aprobadores)
            {
                var yaExiste = await _repo.ExisteNotificacionActivaAsync(
                    NotificacionTipos.TrasladoRegistrado, articuloId, null, usuarioId, trasladoId);

                if (yaExiste)
                    continue;

                await _repo.AddAsync(new Notificacion
                {
                    Tipo = NotificacionTipos.TrasladoRegistrado,
                    Titulo = "Nuevo traslado registrado",
                    Mensaje = mensaje,
                    ArticuloId = articuloId,
                    TrasladoId = trasladoId,
                    UsuarioDestinoId = usuarioId,
                    FechaCreacion = DateTime.Now
                });
            }
        }

        public async Task NotificarTrasladoFirmadoAsync(int trasladoId, int articuloId, int usuarioRegistroId)
        {
            var yaExiste = await _repo.ExisteNotificacionActivaAsync(
                NotificacionTipos.TrasladoFirmado, articuloId, null, usuarioRegistroId, trasladoId);

            if (yaExiste)
                return;

            await _repo.AddAsync(new Notificacion
            {
                Tipo = NotificacionTipos.TrasladoFirmado,
                Titulo = "Traslado firmado",
                Mensaje = $"El traslado #{trasladoId} que registraste ha sido firmado.",
                ArticuloId = articuloId,
                TrasladoId = trasladoId,
                UsuarioDestinoId = usuarioRegistroId,
                FechaCreacion = DateTime.Now
            });
        }

        public async Task ResolverNotificacionesTrasladoAsync(int trasladoId)
        {
            await _repo.MarcarLeidasPorTrasladoAsync(trasladoId);
        }

        private async Task<string> ObtenerDescripcionArticuloAsync(int articuloId)
        {
            var articulo = await _context.Articulos
                .Where(a => a.Id == articuloId)
                .Select(a => new { a.Nombre, a.CodigoPatrimonial })
                .FirstOrDefaultAsync();

            if (articulo == null)
                return $"#{articuloId}";

            var nombre = string.IsNullOrWhiteSpace(articulo.Nombre) ? "Sin nombre" : articulo.Nombre;
            var codigo = string.IsNullOrWhiteSpace(articulo.CodigoPatrimonial) ? "sin código" : articulo.CodigoPatrimonial;

            return $"{nombre} (Código patrimonial: {codigo})";
        }

        private async Task<int?> ResolverUsuarioIdAsync(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            return await _context.Usuarios
                .Where(u => u.Username == username)
                .Select(u => (int?)u.Id)
                .FirstOrDefaultAsync();
        }

        private async Task<List<int>> ObtenerUsuariosAprobadoresAsync()
        {
            return await _context.Usuarios
                .Where(u => u.Estado && NotificacionTipos.RolesAprobadores.Contains(u.Rol.Nombre))
                .Select(u => u.Id)
                .ToListAsync();
        }

        private static NotificacionDto MapToDto(Notificacion n) => new()
        {
            Id = n.Id,
            Tipo = n.Tipo,
            Titulo = n.Titulo,
            Mensaje = n.Mensaje,
            ArticuloId = n.ArticuloId,
            PrestamoId = n.PrestamoId,
            TrasladoId = n.TrasladoId,
            FechaCreacion = n.FechaCreacion,
            Leido = n.Leido,
            FechaLectura = n.FechaLectura
        };
    }
}
