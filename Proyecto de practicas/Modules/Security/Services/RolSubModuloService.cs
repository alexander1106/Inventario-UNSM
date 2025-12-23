using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Security.Services.IServices;
using Proyecto_de_practicas.Data;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_de_practicas.Modules.Security.Services
{
    public class RolSubModuloService :  IRolSubModuloService
    {
        private readonly IRolSubModuloRepository _repository;
        private readonly AplicationDBContext _context;

        public RolSubModuloService(IRolSubModuloRepository repository, AplicationDBContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<RolSubModuloDto?> GetAsync(int rolId, int subModuloId)
        {
            var entity = await _repository.GetByIdAsync(rolId, subModuloId);
            if (entity == null) return null;

            return MapToDto(entity);
        }

        public async Task<List<SubModuloDTO>> GetByRolAsync(int rolId)
        {
            return await _repository.GetSubModulosByRolAsync(rolId);
        }

        public async Task<RolSubModuloDto> CreateAsync(int rolId, int subModuloId)
        {
            var entity = new RolSubModulo
            {
                RolId = rolId,
                SubModuloId = subModuloId
            };

            await _repository.AddAsync(entity);
            return MapToDto(entity);
        }

        public async Task<RolSubModuloDto?> UpdateAsync(RolSubModuloDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.RolId, dto.SubModuloId);
            if (entity == null) return null;

            entity.RolId = dto.RolId;
            entity.SubModuloId = dto.SubModuloId;

            await _repository.UpdateAsync(entity);
            return MapToDto(entity);
        }
        public async Task<List<ModuloConSubModulosDto>> GetModulosConSubModulosPorRolAsync(int rolId)
        {
            var subModulos = await _repository.GetSubModulosByRolAsync(rolId);

            var moduloIds = subModulos.Select(s => s.ModuloId).Distinct().ToList();
            var modulos = await _context.Modulos
                .Where(m => moduloIds.Contains(m.Id))
                .ToListAsync();

            var modulosAgrupados = modulos.Select(m => new ModuloConSubModulosDto
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

            return modulosAgrupados;
        }

        public async Task<bool> DeleteAsync(int rolId, int subModuloId)
        {
            await _repository.DeleteAsync(rolId, subModuloId);
            return true;
        }

        public async Task ActualizarSubModulosAsync(int rolId, List<int> subModulosIds)
        {
            await _repository.DeleteByRolIdAsync(rolId);

            if (subModulosIds != null && subModulosIds.Any())
            {
                var list = subModulosIds.Select(id => new RolSubModulo
                {
                    RolId = rolId,
                    SubModuloId = id
                }).ToList();

                await _repository.AddRangeAsync(list);
            }
        }

        private RolSubModuloDto MapToDto(RolSubModulo entity)
        {
            return new RolSubModuloDto
            {
                Id = entity.Id,
                RolId = entity.RolId,
                SubModuloId = entity.SubModuloId
            };
        }
    }
}