using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;

namespace Proyecto_de_practicas.Modules.Articulos.Services
{
    // Recalcula y persiste el ValorActual de todos los artículos periódicamente,
    // así el valor se va descontando solo a medida que pasan los años, sin que nadie tenga que editar el artículo.
    public class RecalculoValorActualBackgroundService : BackgroundService
    {
        private static readonly TimeSpan Intervalo = TimeSpan.FromHours(24);

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RecalculoValorActualBackgroundService> _logger;

        public RecalculoValorActualBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<RecalculoValorActualBackgroundService> logger)
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

                    var articulos = await context.Articulos.ToListAsync(stoppingToken);

                    foreach (var articulo in articulos)
                    {
                        articulo.ValorActual = DepreciacionCalculator.CalcularValorActual(
                            articulo.ValorAdquisitivo,
                            articulo.FechaAdquision,
                            articulo.DepreciacionAnual);
                    }

                    await context.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al recalcular el ValorActual de los artículos.");
                }

                await Task.Delay(Intervalo, stoppingToken);
            }
        }
    }
}
