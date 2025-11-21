using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Modules.Security.Services
{
    public class RolSubModuloService : IRolSubModuloService
    {
        private readonly IRolSubModuloRepository _repository;

        public RolSubModuloService(IRolSubModuloRepository repository)
        {
            _repository = repository;
        }

        // Obtener un registro por rolId y subModuloId
        public async Task<RolSubModuloDto?> GetAsync(int rolId, int subModuloId)
        {
            var entity = await _repository.GetByIdAsync(rolId, subModuloId);
            if (entity == null) return null;

            return MapToDto(entity);
        }

        // Obtener todos los registros de un rol
        public async Task<IEnumerable<RolSubModuloDto>> GetByRolAsync(int rolId)
        {
            var list = await _repository.GetByRolAsync(rolId);
            return list.Select(MapToDto);
        }

        // Crear nuevo registro
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

        // Actualizar un registro existente usando DTO
        public async Task<RolSubModuloDto?> UpdateAsync(RolSubModuloDto dto)
        {
            // Obtener entidad existente
            var entity = await _repository.GetByIdAsync(dto.RolId, dto.SubModuloId);
            if (entity == null) return null;

            // Actualizar campos necesarios (en este caso solo IDs)
            entity.RolId = dto.RolId;
            entity.SubModuloId = dto.SubModuloId;

            await _repository.UpdateAsync(entity);
            return MapToDto(entity);
        }

        // Eliminar un registro
        public async Task<bool> DeleteAsync(int rolId, int subModuloId)
        {
            await _repository.DeleteAsync(rolId, subModuloId);
            return true;
        }

        // Método privado para mapear entidad a DTO
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
