using Proyecto_de_practicas.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Proyecto_de_practicas.Modules.Reportes.DTO;
using Proyecto_de_practicas.Modules.Reportes.Repository.IReporteRepository;

namespace Proyecto_de_practicas.Modules.Reportes.Repository
{
    public class ReportesRepository : IReportesRepository
    {
        private readonly AplicationDBContext _context;

        public ReportesRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ArticulosPorUbicacionDto>> GetArticulosPorUbicacionAsync()
        {
            return await _context.Ubicaciones
                .Select(u => new ArticulosPorUbicacionDto
                {
                    Ubicacion = u.Nombre,
                    Cantidad = u.Articulos.Count()
                })
                .ToListAsync();
        }

        public async Task<List<ArticulosPorTipoDto>> GetArticulosPorTipoAsync()
        {
            return await _context.Articulos
                .Include(a => a.TipoArticulo)
                .GroupBy(a => a.TipoArticulo.Nombre)
                .Select(g => new ArticulosPorTipoDto
                {
                    Tipo = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();
        }
    }
}
