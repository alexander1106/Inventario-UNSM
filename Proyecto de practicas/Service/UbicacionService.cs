using AutoMapper;
using Proyecto_de_practicas.DTOs;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;

namespace Proyecto_de_practicas.Service
{
    public class UbicacionService : IUbicacionService
    {
        private readonly IUbicacionRepository _repo;
        private readonly IMapper _mapper;

        public UbicacionService(IUbicacionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<UbicacionDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<UbicacionDto>>(entities);
        }

        public async Task<UbicacionDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<UbicacionDto>(entity);
        }

        public async Task<UbicacionDto> AddAsync(UbicacionDto dto)
        {
            // Validar duplicados por nombre y tipo
            var existentes = await _repo.GetAllAsync();
            if (existentes.Any(u =>
                u.Nombre.ToLower() == dto.Nombre.ToLower() &&
                u.TipoUbicacionId == dto.TipoUbicacionId))
            {
                throw new InvalidOperationException("Ya existe una ubicación con ese nombre en este tipo.");
            }

            var entity = _mapper.Map<Ubicacion>(dto);
            var result = await _repo.AddAsync(entity);
            return _mapper.Map<UbicacionDto>(result);
        }

        public async Task<UbicacionDto> UpdateAsync(int id, UbicacionDto dto)
        {
            var existentes = await _repo.GetAllAsync();
            if (existentes.Any(u =>
                u.Id != id &&
                u.Nombre.ToLower() == dto.Nombre.ToLower() &&
                u.Descripcion == dto.Descripcion))
            {
                throw new InvalidOperationException("Ya existe una ubicación con ese nombre en este tipo.");
            }

            // 🔧 Recuperar la entidad existente del contexto
            var existingEntity = await _repo.GetByIdAsync(id);
            if (existingEntity == null)
                throw new InvalidOperationException("No se encontró la ubicación a actualizar.");

            // 🔄 Mapear los cambios al objeto rastreado
            _mapper.Map(dto, existingEntity);

            var result = await _repo.UpdateAsync(existingEntity);
            return _mapper.Map<UbicacionDto>(result);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
