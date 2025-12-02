using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Modules.Security.Services
{
    public class RolSubModuloPermisoService : IRolSubModuloPermisoService
    {
        private readonly IRolSubModuloPermisoRepository _repository;

        public RolSubModuloPermisoService(IRolSubModuloPermisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<RolSubModuloPermisoDto?> GetAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return MapToDto(entity);
        }

        public async Task<IEnumerable<RolSubModuloPermisoDto>> GetByRolSubModuloAsync(int rolSubModuloId)
        {
            var list = await _repository.GetByRolSubModuloAsync(rolSubModuloId);
            return list.Select(MapToDto);
        }

        public async Task<RolSubModuloPermisoDto> CreateAsync(int rolSubModuloId, int permisoId)
        {
            var entity = new RolSubModuloPermiso
            {
                RolSubModuloId = rolSubModuloId,
                PermisoId = permisoId
            };

            await _repository.AddAsync(entity);
            return MapToDto(entity);
        }

        public async Task<RolSubModuloPermisoDto?> UpdateAsync(RolSubModuloPermisoDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) return null;

            entity.RolSubModuloId = dto.RolSubModuloId;
            entity.PermisoId = dto.PermisoId;

            await _repository.UpdateAsync(entity);
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }

        private RolSubModuloPermisoDto MapToDto(RolSubModuloPermiso entity)
        {
            return new RolSubModuloPermisoDto
            {
                Id = entity.Id,
                RolSubModuloId = entity.RolSubModuloId,
                PermisoId = entity.PermisoId
            };
        }
    }
}