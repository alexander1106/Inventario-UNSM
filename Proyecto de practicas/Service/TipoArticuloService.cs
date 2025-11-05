using AutoMapper;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository.IRepository;

namespace Proyecto_de_practicas.Service
{
    public class TipoArticuloService : ITipoArticuloService
    {
        private readonly ITipoArticuloRepository _repo;
        private readonly IMapper _mapper;

        public TipoArticuloService(ITipoArticuloRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TipoArticuloDTO>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<TipoArticuloDTO>>(entities);
        }

        public async Task<TipoArticuloDTO?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TipoArticuloDTO>(entity);
        }

        public async Task<TipoArticuloDTO> AddAsync(TipoArticuloDTO dto)
        {
            var entity = _mapper.Map<TipoArticulo>(dto);
            var result = await _repo.AddAsync(entity);
            return _mapper.Map<TipoArticuloDTO>(result);
        }

        public async Task<TipoArticuloDTO> UpdateAsync(int id, TipoArticuloDTO dto)
        {
            // Validar relación
            bool tieneRelacion = await _repo.TieneRelacionConArticulosAsync(id);
            if (tieneRelacion)
                throw new InvalidOperationException("No se puede editar este tipo de artículo porque tiene artículos relacionados.");

            var entity = _mapper.Map<TipoArticulo>(dto);
            entity.Id = id;
            var result = await _repo.UpdateAsync(entity);
            return _mapper.Map<TipoArticuloDTO>(result);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            // Validar relación
            bool tieneRelacion = await _repo.TieneRelacionConArticulosAsync(id);
            if (tieneRelacion)
                throw new InvalidOperationException("No se puede eliminar este tipo de artículo porque tiene artículos relacionados.");

            return await _repo.DeleteAsync(id);
        }


        public async Task<TipoArticuloDTO?> ObtenerPorIdAsync(int id)
        {
            var entity = await _repo.GetByIdWithArticulosAsync(id);
            if (entity == null) return null;

            return _mapper.Map<TipoArticuloDTO>(entity);
        }
    }
}