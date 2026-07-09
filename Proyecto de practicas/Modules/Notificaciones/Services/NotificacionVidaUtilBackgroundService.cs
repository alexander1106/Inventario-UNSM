using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;

namespace Proyecto_de_practicas.Modules.Notificaciones.Services
{
    // Revisa periódicamente los artículos y genera una notificación (para cada usuario aprobador)
    // cuando a su vida útil le quedan pocos días (o ya venció), sin duplicar avisos ya pendientes de leer.
    public class NotificacionVidaUtilBackgroundService : BackgroundService
    {
        private static readonly TimeSpan Intervalo = TimeSpan.FromHours(24);
        private const int DiasAnticipacion = 30;

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificacionVidaUtilBackgroundService> _logger;

        public NotificacionVidaUtilBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<NotificacionVidaUtilBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AplicationDBContext>();
                    var notificacionService = scope.ServiceProvider.GetRequiredService<INotificacionService>();

                    var hoy = DateTime.Now.Date;
                    var articulos = await context.Articulos
                        .Where(a => a.Estado == 1 && a.TiempoVidaUtil > 0)
                        .ToListAsync(stoppingToken);

                    foreach (var articulo in articulos)
                    {
                        var fechaFinVidaUtil = articulo.FechaAdquision.AddDays(articulo.TiempoVidaUtil * 365.25);
                        var diasRestantes = (fechaFinVidaUtil.Date - hoy).TotalDays;

                        if (diasRestantes > DiasAnticipacion)
                            continue;

                        var mensaje = diasRestantes < 0
                            ? $"La vida útil de \"{articulo.Nombre}\" venció el {fechaFinVidaUtil:dd/MM/yyyy}."
                            : $"La vida útil de \"{articulo.Nombre}\" vence el {fechaFinVidaUtil:dd/MM/yyyy} (en {(int)diasRestantes} días).";

                        await notificacionService.NotificarVidaUtilProximaAsync(articulo.Id, mensaje);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al generar notificaciones de vida útil próxima a vencer.");
                }

                await Task.Delay(Intervalo, stoppingToken);
            }
        }
    }
}
