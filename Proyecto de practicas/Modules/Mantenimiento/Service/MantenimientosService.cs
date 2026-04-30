using global::Proyecto_de_practicas.Data;
using global::Proyecto_de_practicas.Modules.Mantenimiento.DTO;
using global::Proyecto_de_practicas.Modules.Mantenimiento.Entity;
using global::Proyecto_de_practicas.Modules.Mantenimiento.Service.IService;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Mantenimiento.DTO;
using Proyecto_de_practicas.Modules.Mantenimiento.Entity;
using Proyecto_de_practicas.Modules.Mantenimiento.Service.IService;

namespace Proyecto_de_practicas.Modules.Mantenimiento.Service
{
    public class MantenimientosService : IMantenimientosService
    {
        private readonly AplicationDBContext _context;

        public MantenimientosService(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Mantenimientos>> GetAll()
        {
            return await _context.Set<Mantenimientos>()
                .Include(m => m.Articulo)
                .Where(m => m.Estado)
                .ToListAsync();
        }

        public async Task<Mantenimientos?> GetById(int id)
        {
            return await _context.Set<Mantenimientos>()
                .Include(m => m.Articulo)
                .FirstOrDefaultAsync(m => m.Id == id && m.Estado);
        }

        public async Task<Mantenimientos> Create(MantenimientosCreateDTO dto)
        {
            var mantenimiento = new Mantenimientos
            {
                ArticuloId = dto.ArticuloId,
                FechaMantenimiento = dto.FechaMantenimiento,
                ProveedorServicion = dto.ProveedorServicion,
                Costo = dto.Costo,
                TipoMantenimiento = dto.TipoMantenimiento,
                Observaciones = dto.Observaciones,
                EstadoMantenimiento = true,
                Estado = true
            };

            _context.Add(mantenimiento);
            await _context.SaveChangesAsync();

            return mantenimiento;
        }

        public async Task<bool> UpdateEstado(int id, MantenimientosUpdateDTO dto)
        {
            var mantenimiento = await _context.Set<Mantenimientos>().FindAsync(id);

            if (mantenimiento == null) return false;

            mantenimiento.ArticuloId = dto.ArticuloId;
            mantenimiento.FechaMantenimiento = dto.FechaMantenimiento;
            mantenimiento.ProveedorServicion = dto.ProveedorServicion;
            mantenimiento.Costo = dto.Costo;
            mantenimiento.TipoMantenimiento = dto.TipoMantenimiento;
            mantenimiento.Observaciones = dto.Observaciones;
            mantenimiento.EstadoMantenimiento = dto.EstadoMantenimiento;

            _context.Update(mantenimiento);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var mantenimiento = await _context.Set<Mantenimientos>().FindAsync(id);

            if (mantenimiento == null) return false;

            mantenimiento.Estado = false;

            _context.Update(mantenimiento);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}