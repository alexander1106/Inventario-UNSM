using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Security.Services.IServices;
using Proyecto_de_practicas.Data;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_de_practicas.Modules.Security.Services
{
    public class RolPermisosService : IRolPermisosService
    {
        private readonly IRolPermisoRepository _repository;
        private readonly AplicationDBContext _context;

        public RolPermisosService(IRolPermisoRepository repository, AplicationDBContext context)
        {
            _repository = repository;
            _context = context;
        }

        // ✅ GET UNO
        public async Task<RolPermisosDTO?> GetAsync(int rolId, int subModuloId, int permisoId)
        {
            var entity = await _context.RolPermisos
                .FirstOrDefaultAsync(x =>
                    x.RolId == rolId &&
                    x.SubModuloId == subModuloId &&
                    x.PermisoId == permisoId);

            return entity == null ? null : MapToDto(entity);
        }

        // ✅ GET POR ROL
        public async Task<List<SubModuloDTO>> GetByRolAsync(int rolId)
        {
            return await _repository.GetSubModulosByRolAsync(rolId);
        }

        // ✅ CREATE
        public async Task<RolPermisosDTO> CreateAsync(RolPermisosDTO dto)
        {
            var exists = await _context.RolPermisos.AnyAsync(x =>
                x.RolId == dto.RolId &&
                x.SubModuloId == dto.SubModuloId &&
                x.PermisoId == dto.PermisoId);

            if (exists)
                throw new Exception("El permiso ya está asignado a este rol");

            var entity = new RolPermisos
            {
                RolId = dto.RolId,
                SubModuloId = dto.SubModuloId,
                PermisoId = dto.PermisoId
            };

            await _repository.AddAsync(entity);
            return MapToDto(entity);
        }

        // ✅ UPDATE INDIVIDUAL (🔥 NUEVO)
        public async Task<RolPermisosDTO?> UpdateByIdAsync(int id, RolPermisosDTO dto)
        {
            var entity = await _context.RolPermisos.FindAsync(id);

            if (entity == null)
                throw new Exception("No existe el permiso");

            var exists = await _context.RolPermisos.AnyAsync(x =>
                x.Id != id &&
                x.RolId == dto.RolId &&
                x.SubModuloId == dto.SubModuloId &&
                x.PermisoId == dto.PermisoId);

            if (exists)
                throw new Exception("Ya existe ese permiso");

            entity.RolId = dto.RolId;
            entity.SubModuloId = dto.SubModuloId;
            entity.PermisoId = dto.PermisoId;

            await _context.SaveChangesAsync();

            return MapToDto(entity);
        }

        // 🚀 SYNC INTELIGENTE (🔥 NUEVO)
        public async Task SyncPermisosAsync(int rolId, List<RolPermisosDTO> permisos)
        {
            var actuales = await _context.RolPermisos
                .Where(x => x.RolId == rolId)
                .ToListAsync();

            var nuevos = permisos
                .GroupBy(p => new { p.SubModuloId, p.PermisoId })
                .Select(g => g.First())
                .ToList();

            var eliminar = actuales
                .Where(a => !nuevos.Any(n =>
                    n.SubModuloId == a.SubModuloId &&
                    n.PermisoId == a.PermisoId))
                .ToList();

            var agregar = nuevos
                .Where(n => !actuales.Any(a =>
                    a.SubModuloId == n.SubModuloId &&
                    a.PermisoId == n.PermisoId))
                .Select(n => new RolPermisos
                {
                    RolId = rolId,
                    SubModuloId = n.SubModuloId,
                    PermisoId = n.PermisoId
                })
                .ToList();

            if (eliminar.Any())
                _context.RolPermisos.RemoveRange(eliminar);

            if (agregar.Any())
                await _context.RolPermisos.AddRangeAsync(agregar);

            await _context.SaveChangesAsync();
        }

        // ✅ DELETE
        public async Task<bool> DeleteAsync(int rolId, int subModuloId, int permisoId)
        {
            var entity = await _context.RolPermisos
                .FirstOrDefaultAsync(x =>
                    x.RolId == rolId &&
                    x.SubModuloId == subModuloId &&
                    x.PermisoId == permisoId);

            if (entity == null) return false;

            await _repository.DeleteAsync(rolId, subModuloId, permisoId);
            return true;
        }

        // ✅ VISTA COMPLETA
        public async Task<List<ModuloConSubModulosDto>> GetModulosConSubModulosPorRolAsync(int rolId)
        {
            var subModulos = await _repository.GetSubModulosByRolAsync(rolId);

            var moduloIds = subModulos.Select(s => s.ModuloId).Distinct().ToList();

            var modulos = await _context.Modulos
                .Where(m => moduloIds.Contains(m.Id))
                .ToListAsync();

            return modulos.Select(m => new ModuloConSubModulosDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Ruta = m.Ruta,
                Icon = m.Icon,
                SubModulos = subModulos
                    .Where(s => s.ModuloId == m.Id)
                    .Select(s => new SubModuloDTO
                    {
                        Id = s.Id,
                        Nombre = s.Nombre,
                        Ruta = s.Ruta,
                        Icon = s.Icon
                    }).ToList()
            }).ToList();
        }

        private RolPermisosDTO MapToDto(RolPermisos entity)
        {
            return new RolPermisosDTO
            {
                Id = entity.Id,
                RolId = entity.RolId,
                SubModuloId = entity.SubModuloId,
                PermisoId = entity.PermisoId
            };
        }

        public async Task<RolAccesoDTO> GetAccesosPorRolAsync(int rolId)
        {
            var data = await _context.RolPermisos
                .Where(rp => rp.RolId == rolId)
                .Include(rp => rp.SubModulo)
                    .ThenInclude(sm => sm.Modulo)
                .Include(rp => rp.Permiso)
                .ToListAsync();

            var resultado = new RolAccesoDTO
            {
                RolId = rolId,
                Modulos = data
                    .GroupBy(rp => rp.SubModulo.Modulo)
                    .Select(mod => new ModuloDTO
                    {
                        Id = mod.Key.Id,
                        Nombre = mod.Key.Nombre,
                        Icon = mod.Key.Icon,
                        SubModulos = mod
                            .GroupBy(rp => rp.SubModulo)
                            .Select(sub => new SubModuloDTO
                            {
                                Id = sub.Key.Id,
                                Nombre = sub.Key.Nombre,
                                Ruta = sub.Key.Ruta,
                                Icon = sub.Key.Icon,
                                ModuloId = sub.Key.ModuloId,
                                Permisos = sub
                                    .Select(rp => rp.Permiso.Nombre)
                                    .Distinct()
                                    .ToList()
                            })
                            .ToList()
                    })
                    .ToList()
            };

            return resultado;
        }

    }
}