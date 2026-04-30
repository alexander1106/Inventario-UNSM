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

        // ✅ GET UNO (CORREGIDO)
        public async Task<RolPermisosDTO?> GetAsync(int rolId, int? moduloId, int? subModuloId, int permisoId)
        {
            var entity = await _repository.GetByKeysAsync(rolId, moduloId, subModuloId, permisoId);
            return entity == null ? null : MapToDto(entity);
        }

        // ✅ GET POR ROL
        public async Task<List<SubModuloDTO>> GetByRolAsync(int rolId)
        {
            return await _repository.GetSubModulosByRolAsync(rolId);
        }

        // ✅ CREATE (CORREGIDO)
        public async Task<RolPermisosDTO> CreateAsync(RolPermisosDTO dto)
        {
            if (dto.ModuloId == null && dto.SubModuloId == null)
                throw new Exception("Debe enviar ModuloId o SubModuloId");

            if (dto.ModuloId != null && dto.SubModuloId != null)
                throw new Exception("No puede enviar ModuloId y SubModuloId al mismo tiempo");

            var exists = await _context.RolPermisos.AnyAsync(x =>
                x.RolId == dto.RolId &&
                x.ModuloId == dto.ModuloId &&
                x.SubModuloId == dto.SubModuloId &&
                x.PermisoId == dto.PermisoId);

            if (exists)
                throw new Exception("El permiso ya existe");

            var entity = new RolPermisos
            {
                RolId = dto.RolId,
                ModuloId = dto.ModuloId,
                SubModuloId = dto.SubModuloId,
                PermisoId = dto.PermisoId
            };

            await _repository.AddAsync(entity);

            return MapToDto(entity);
        }

        // ✅ UPDATE INDIVIDUAL (CORREGIDO)
        public async Task<RolPermisosDTO?> UpdateByIdAsync(int id, RolPermisosDTO dto)
        {
            var entity = await _context.RolPermisos.FindAsync(id);

            if (entity == null)
                throw new Exception("No existe el permiso");

            if (dto.ModuloId == null && dto.SubModuloId == null)
                throw new Exception("Debe enviar ModuloId o SubModuloId");

            if (dto.ModuloId != null && dto.SubModuloId != null)
                throw new Exception("No puede enviar ambos");

            var exists = await _context.RolPermisos.AnyAsync(x =>
                x.Id != id &&
                x.RolId == dto.RolId &&
                x.ModuloId == dto.ModuloId &&
                x.SubModuloId == dto.SubModuloId &&
                x.PermisoId == dto.PermisoId);

            if (exists)
                throw new Exception("Ya existe ese permiso");

            entity.RolId = dto.RolId;
            entity.ModuloId = dto.ModuloId;
            entity.SubModuloId = dto.SubModuloId;
            entity.PermisoId = dto.PermisoId;

            await _context.SaveChangesAsync();

            return MapToDto(entity);
        }

        // 🚀 SYNC INTELIGENTE (CORREGIDO)
        public async Task SyncPermisosAsync(int rolId, List<RolPermisosDTO> permisos)
        {
            var actuales = await _context.RolPermisos
                .Where(x => x.RolId == rolId)
                .ToListAsync();

            var nuevos = permisos
                .GroupBy(p => new { p.ModuloId, p.SubModuloId, p.PermisoId })
                .Select(g => g.First())
                .ToList();

            var eliminar = actuales
                .Where(a => !nuevos.Any(n =>
                    n.ModuloId == a.ModuloId &&
                    n.SubModuloId == a.SubModuloId &&
                    n.PermisoId == a.PermisoId))
                .ToList();

            var agregar = nuevos
                .Where(n => !actuales.Any(a =>
                    a.ModuloId == n.ModuloId &&
                    a.SubModuloId == n.SubModuloId &&
                    a.PermisoId == n.PermisoId))
                .Select(n => new RolPermisos
                {
                    RolId = rolId,
                    ModuloId = n.ModuloId,
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

        // ✅ DELETE (CORREGIDO)
        public async Task<bool> DeleteAsync(int rolId, int? moduloId, int? subModuloId, int permisoId)
        {
            var entity = await _repository.GetByKeysAsync(rolId, moduloId, subModuloId, permisoId);

            if (entity == null) return false;

            _context.RolPermisos.Remove(entity);
            await _context.SaveChangesAsync();

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
                    .ToList()
            }).ToList();
        }

        private RolPermisosDTO MapToDto(RolPermisos entity)
        {
            return new RolPermisosDTO
            {
                Id = entity.Id,
                RolId = entity.RolId,
                ModuloId = entity.ModuloId,
                SubModuloId = entity.SubModuloId,
                PermisoId = entity.PermisoId
            };
        }

        // ✅ ACCESOS COMPLETOS (CORREGIDO PARA MODULO + SUBMODULO)
        public async Task<RolAccesoDTO> GetAccesosPorRolAsync(int rolId)
        {
            var data = await _context.RolPermisos
                .Where(rp => rp.RolId == rolId)
                .Include(rp => rp.Modulo)
                .Include(rp => rp.SubModulo)
                    .ThenInclude(sm => sm.Modulo)
                .Include(rp => rp.Permiso)
                .ToListAsync();

            var resultado = new RolAccesoDTO
            {
                RolId = rolId,
                Modulos = data
                    .GroupBy(rp => rp.Modulo ?? rp.SubModulo.Modulo)
                    .Select(mod => new ModuloDTO
                    {
                        Id = mod.Key.Id,
                        Nombre = mod.Key.Nombre,
                        Icon = mod.Key.Icon,
                        Ruta = !string.IsNullOrEmpty(mod.Key.Ruta)
                        ? mod.Key.Ruta
                        : mod
                            .Where(rp => rp.SubModulo != null && rp.SubModulo.Ruta != null)
                            .Select(rp => rp.SubModulo.Ruta)
                            .FirstOrDefault(),

                        SubModulos = mod
                            .Where(rp => rp.SubModulo != null)
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