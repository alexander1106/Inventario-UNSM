using AutoMapper;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;

namespace Proyecto_de_practicas.Service
{
    public class TipoUbicacionService : ITipoUbicacionService
    {
        private readonly ITipoUbicacionRepository _repo;
        private readonly IMapper _mapper;

        public TipoUbicacionService(ITipoUbicacionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TipoUbicacion>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<TipoUbicacion>>(entities);
        }

        public async Task<TipoUbicacion?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TipoUbicacion>(entity);
        }

        public async Task<TipoUbicacion> AddAsync(TipoUbicacion tipoUbicacion)
        {
            // Validación de duplicado por nombre
            var existentes = await _repo.GetAllAsync();
            if (existentes.Any(t => t.Nombre.Trim().ToLower() == tipoUbicacion.Nombre.Trim().ToLower()))
                throw new InvalidOperationException("Ya existe un tipo de ubicación con ese nombre.");

            var entity = _mapper.Map<TipoUbicacion>(tipoUbicacion);
            var result = await _repo.AddAsync(entity);
            return _mapper.Map<TipoUbicacion>(result);
        }

        public async Task<TipoUbicacion> UpdateAsync(int id, TipoUbicacion tipoUbicacion)
        {
            var existentes = await _repo.GetAllAsync();
            if (existentes.Any(t => t.Id != id && t.Nombre.Trim().ToLower() == tipoUbicacion.Nombre.Trim().ToLower()))
                throw new InvalidOperationException("Ya existe un tipo de ubicación con ese nombre.");

            var entity = _mapper.Map<TipoUbicacion>(tipoUbicacion);
            entity.Id = id;
            var result = await _repo.UpdateAsync(entity);
            return _mapper.Map<TipoUbicacion>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tipo = await _repo.GetByIdAsync(id);
            if (tipo == null) return false;

            if (tipo.Ubicaciones != null && tipo.Ubicaciones.Any())
            {
                throw new InvalidOperationException("No se puede eliminar un tipo de ubicación que tiene ubicaciones asociadas.");
            }

            return await _repo.DeleteAsync(id);
        }
    }
}
