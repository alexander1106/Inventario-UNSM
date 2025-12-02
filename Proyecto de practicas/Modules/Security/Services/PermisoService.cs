using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Modules.Security.Services
{
    public class PermisoService : IPermisoService
    {
        private readonly IPermisoRepository _repository;

        public PermisoService(IPermisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<PermisoDto?> GetAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return MapToDto(entity);
        }

        public async Task<IEnumerable<PermisoDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(MapToDto);
        }

        public async Task<PermisoDto> CreateAsync(PermisoDto dto)
        {
            var entity = new Permiso
            {
                Nombre = dto.Nombre,
                Activo = dto.Activo
            };

            await _repository.AddAsync(entity);
            return MapToDto(entity);
        }

        public async Task<PermisoDto?> UpdateAsync(PermisoDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) return null;

            entity.Nombre = dto.Nombre;
            entity.Activo = dto.Activo;

            await _repository.UpdateAsync(entity);
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }

        private PermisoDto MapToDto(Permiso entity)
        {
            return new PermisoDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Activo = entity.Activo
            };
        }
    }
}