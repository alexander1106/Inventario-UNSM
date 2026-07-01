using AutoMapper;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services
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

        public async Task<List<UbicacionDto>> GetByPadreAsync(int padreId)
        {
            var entities = await _repo.GetAllAsync();

            var filtradas = entities
                .ToList();

            return _mapper.Map<List<UbicacionDto>>(filtradas);
        }
        public async Task<List<UbicacionDto>> GetByUsuarioAsync(int usuarioId)
        {
            var entidades = await _repo.GetAllAsync();

            var filtradas = entidades
                .Where(u => u.UsuarioId == usuarioId)
                .ToList();

            return _mapper.Map<List<UbicacionDto>>(filtradas);
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
            var existentes = await _repo.GetAllAsync();

            if (existentes.Any(u =>
                u.Nombre.ToLower() == dto.Nombre.ToLower() &&
                u.TipoUbicacionId == dto.TipoUbicacionId))
            {
                throw new InvalidOperationException(
                    "El nombre ingresado ya corresponde a una ubicación existente en este tipo");
            }

            var entity = new Ubicacion
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Piso = dto.Piso,
                TipoUbicacionId = dto.TipoUbicacionId,
                UsuarioId = dto.UsuarioId,
                EscuelaId = dto.EscuelaId,   // ✅ FIX IMPORTANTE
            };

            var result = await _repo.AddAsync(entity);
            return _mapper.Map<UbicacionDto>(result);
        }

        public async Task<UbicacionDto> UpdateAsync(int id, UbicacionDto dto)
        {
            var existentes = await _repo.GetAllAsync();

            if (existentes.Any(u =>
                u.Id != id &&
                u.Nombre.ToLower() == dto.Nombre.ToLower() &&
                u.TipoUbicacionId == dto.TipoUbicacionId))
            {
                throw new InvalidOperationException(
                    "El nombre ingresado ya corresponde a una ubicación existente en este tipo");
            }

            var existingEntity = await _repo.GetByIdAsync(id);

            if (existingEntity == null)
                throw new InvalidOperationException("No se encontró la ubicación a actualizar.");
            
            existingEntity.Nombre = dto.Nombre;
            existingEntity.Descripcion = dto.Descripcion;
            existingEntity.Piso = dto.Piso;
            existingEntity.TipoUbicacionId = dto.TipoUbicacionId;

            existingEntity.EscuelaId = dto.EscuelaId; // ✅ FIX IMPORTANTE
            existingEntity.UsuarioId = dto.UsuarioId;

            var result = await _repo.UpdateAsync(existingEntity);
            return _mapper.Map<UbicacionDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<UbicacionDto>> GetByTipoAsync(int tipoId)
        {
            var entities = await _repo.GetAllAsync();

            var filtradas = entities
                .Where(u => u.TipoUbicacionId == tipoId)
                .ToList();

            return _mapper.Map<List<UbicacionDto>>(filtradas);
        }
        public async Task<UbicacionDto> AsignarUsuarioAsync(int ubicacionId, int usuarioId)
        {
            var ubicacion = await _repo.GetByIdAsync(ubicacionId);

            if (ubicacion == null)
                throw new Exception("Ubicación no encontrada");

            ubicacion.UsuarioId = usuarioId;

            var result = await _repo.UpdateAsync(ubicacion);

            return _mapper.Map<UbicacionDto>(result);
        }

        public async Task<List<UbicacionDto>> GetByEscuelaIdAsync(int escuelaId)
        {
            var entities = await _repo.GetAllAsync();
            var filtradas = entities
                .Where(u => u.EscuelaId == escuelaId)
                .ToList();
            return _mapper.Map<List<UbicacionDto>>(filtradas);
        }
    }
}