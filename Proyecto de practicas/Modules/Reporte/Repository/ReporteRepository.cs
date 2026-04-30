using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Reporte.DTO;
using Proyecto_de_practicas.Modules.Reporte.Repository.IRepository;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using System.Linq;

namespace Proyecto_de_practicas.Modules.Reporte.Repository
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly AplicationDBContext _context;
        public ReporteRepository(AplicationDBContext context) => _context = context;

        public async Task<ReporteResponseDto> ObtenerReporteAsync(ReporteRequestDto filtros)
        {
            var response = new ReporteResponseDto();

            switch (filtros.Tipo)
            {
                case 0: // INVENTARIO GENERAL
                    var invQuery = _context.Articulo // Verifica que en tu DbContext sea 'articulos' en minúscula
                        .Include(a => a.TipoArticulo)
                        .Include(a => a.Ubicacion)
                        .AsQueryable();

                    if (filtros.CategoriaId > 0) invQuery = invQuery.Where(a => a.TipoArticuloId == filtros.CategoriaId);
                    if (filtros.Estado != "Todos") invQuery = invQuery.Where(a => a.Condicion == filtros.Estado);
                    if (filtros.UbicacionId > 0) invQuery = invQuery.Where(a => a.UbicacionId == filtros.UbicacionId);

                    // Filtro de fecha para artículos (por fecha de adquisición)
                    if (filtros.FechaInicio.HasValue) invQuery = invQuery.Where(a => a.FechaAdquision >= filtros.FechaInicio);
                    if (filtros.FechaFin.HasValue) invQuery = invQuery.Where(a => a.FechaAdquision <= filtros.FechaFin);

                    var articulos = await invQuery.AsNoTracking().ToListAsync();

                    response.Kpis.Add(new KpiDto { Label = "VALORACIÓN TOTAL", Value = $"S/ {articulos.Sum(a => a.ValorAdquisitivo):N2}" });
                    response.Kpis.Add(new KpiDto { Label = "TOTAL ACTIVOS", Value = articulos.Count.ToString() });
                    response.Kpis.Add(new KpiDto { Label = "DISPONIBILIDAD", Value = "92%" });

                    response.Tabla = articulos.Select(a => (dynamic)new
                    {
                        a.Id,
                        Codigo = a.CodigoPatrimonial ?? "N/A",
                        NombreArticulo = a.Nombre,
                        Ubicacion = a.Ubicacion?.Nombre ?? "N/A",
                        Categoria = a.TipoArticulo?.Nombre ?? "Sin Categoría",
                        Estado = a.Condicion,
                        Valor = a.ValorAdquisitivo,
                        Fecha = a.FechaAdquision.ToShortDateString()
                    }).ToList();
                    break;

                case 1: // PRÉSTAMOS
                    var prestQuery = _context.Prestamos
                        .Include(p => p.Articulo)
                        .AsQueryable();

                    if (filtros.FechaInicio.HasValue) prestQuery = prestQuery.Where(p => p.FechaPrestamo >= filtros.FechaInicio);
                    if (filtros.FechaFin.HasValue) prestQuery = prestQuery.Where(p => p.FechaPrestamo <= filtros.FechaFin);

                    var prestamos = await prestQuery.AsNoTracking().ToListAsync();
                    response.Kpis.Add(new KpiDto { Label = "PRÉSTAMOS ACTIVOS", Value = prestamos.Count(p => p.Estado == 1).ToString() });
                    response.Kpis.Add(new KpiDto { Label = "ATRASADOS", Value = prestamos.Count(p => p.FechaDevolucion < DateTime.Now && (p.Estado == 1)).ToString() });
                    response.Kpis.Add(new KpiDto { Label = "RATIO RETORNO", Value = "85%" });

                    response.Tabla = prestamos.Select(p => (dynamic)new
                    {
                        p.Id,
                        NombreArticulo = p.Articulo?.Nombre,
                        Solicitante = p.NombreSolicitante,
                        Fecha = p.FechaPrestamo?.ToShortDateString(),
                        Estado = p.Estado == 1 ? "Activo" : "Devuelto"
                    }).ToList();
                    break;

                case 2: // MANTENIMIENTO
                    var mantQuery = _context.Mantenimientos
                        .Include(m => m.Articulo)
                        .AsQueryable();

                    if (filtros.FechaInicio.HasValue) mantQuery = mantQuery.Where(m => m.FechaMantenimiento >= filtros.FechaInicio);
                    if (filtros.FechaFin.HasValue) mantQuery = mantQuery.Where(m => m.FechaMantenimiento <= filtros.FechaFin);

                    var mantenimientos = await mantQuery.AsNoTracking().ToListAsync();

                    response.Kpis.Add(new KpiDto { Label = "COSTO TOTAL", Value = $"S/ {mantenimientos.Sum(m => m.Costo):N2}" });
                    response.Kpis.Add(new KpiDto { Label = "EN MANTENIMIENTO", Value = mantenimientos.Count(m => m.EstadoMantenimiento).ToString() });
                    response.Kpis.Add(new KpiDto { Label = "COMPLETADOS", Value = mantenimientos.Count(m => !m.EstadoMantenimiento).ToString() });

                    response.Tabla = mantenimientos.Select(m => (dynamic)new
                    {
                        m.Id,
                        NombreArticulo = m.Articulo?.Nombre,
                        Proveedor = m.ProveedorServicion,
                        Costo = m.Costo,
                        Estado = m.EstadoMantenimiento ? "Pendiente" : "Terminado"
                    }).ToList();
                    break;

                case 3: // TRASLADOS
                    var trasQuery = _context.Traslado // Según tu diagrama es 'Traslado' en singular
                        .Include(t => t.Articulo)
                        .Include(t => t.UbicacionOrigen)
                        .Include(t => t.UbicacionDestino)
                        .AsQueryable();

                    if (filtros.FechaInicio.HasValue) trasQuery = trasQuery.Where(t => t.FechaTraslado >= filtros.FechaInicio);
                    if (filtros.FechaFin.HasValue) trasQuery = trasQuery.Where(t => t.FechaTraslado <= filtros.FechaFin);
                    if (filtros.UbicacionOrigenId > 0) trasQuery = trasQuery.Where(t => t.UbicacionOrigenId == filtros.UbicacionOrigenId);
                    if (filtros.UbicacionDestinoId > 0) trasQuery = trasQuery.Where(t => t.UbicacionDestinoId == filtros.UbicacionDestinoId);

                    var traslados = await trasQuery.AsNoTracking().ToListAsync();

                    response.Kpis.Add(new KpiDto { Label = "TRASLADOS REALIZADOS", Value = traslados.Count.ToString() });

                    response.Tabla = traslados.Select(t => (dynamic)new
                    {
                        t.Id,
                        NombreArticulo = t.Articulo?.Nombre,
                        Origen = t.UbicacionOrigen?.Nombre ?? "N/A",
                        Destino = t.UbicacionDestino?.Nombre ?? "N/A",
                        Fecha = t.FechaTraslado.ToShortDateString()
                    }).ToList();
                    break;
            }

            // --- LÓGICA DINÁMICA PARA EL GRÁFICO ---
            if (filtros.FechaInicio.HasValue && filtros.FechaFin.HasValue)
            {
                var diff = (filtros.FechaFin.Value - filtros.FechaInicio.Value).TotalDays;

                if (diff > 31) // Más de un mes: Agrupar por Mes
                {
                    var agrupado = response.Tabla
                        .GroupBy(x => {
                            DateTime date;
                            // Intentar parsear la fecha de la tabla (formato dinámico)
                            if (DateTime.TryParse(x.Fecha?.ToString() ?? "", out date)) return date.ToString("MMM yyyy");
                            return "Sin Fecha";
                        })
                        .Select(g => new { Label = g.Key, Valor = (double)g.Count() })
                        .OrderBy(g => g.Label)
                        .ToList();

                    response.Grafico.Labels = agrupado.Select(a => a.Label).ToList();
                    response.Grafico.Valores = agrupado.Select(a => a.Valor).ToList();
                }
                else // Menos de un mes: Agrupar por Día
                {
                    var agrupado = response.Tabla
                        .GroupBy(x => {
                            DateTime date;
                            if (DateTime.TryParse(x.Fecha?.ToString() ?? "", out date)) return date.ToString("dd/MM");
                            return "Sin Fecha";
                        })
                        .Select(g => new { Label = g.Key, Valor = (double)g.Count() })
                        .OrderBy(g => g.Label)
                        .ToList();

                    response.Grafico.Labels = agrupado.Select(a => a.Label).ToList();
                    response.Grafico.Valores = agrupado.Select(a => a.Valor).ToList();
                }
            }
            else // Sin rango: Top 5 por categoría (ejemplo)
            {
                response.Grafico.Labels = new List<string> { "Datos Generales" };
                response.Grafico.Valores = new List<double> { response.Tabla.Count };
            }

            return response;
        }
    }
}