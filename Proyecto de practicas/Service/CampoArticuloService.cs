using AutoMapper;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;

namespace Proyecto_de_practicas.Service
{
    public class CampoArticuloService : ICampoArticuloService
    {
        private readonly ICampoArticuloRepository _repo;
        private readonly IMapper _mapper;

        public CampoArticuloService(ICampoArticuloRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CampoArticuloDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<CampoArticuloDto>>(entities);
        }

        public async Task<CampoArticuloDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<CampoArticuloDto>(entity);
        }

        public async Task<CampoArticuloDto> AddAsync(CampoArticuloDto dto)
        {
            var entity = _mapper.Map<CampoArticulo>(dto);
            var result = await _repo.AddAsync(entity);
            return _mapper.Map<CampoArticuloDto>(result);
        }

        public async Task<CampoArticuloDto> UpdateAsync(int id, CampoArticuloDto dto)
        {
            var entity = _mapper.Map<CampoArticulo>(dto);
            entity.Id = id;
            var result = await _repo.UpdateAsync(entity);
            return _mapper.Map<CampoArticuloDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<CampoArticuloDto>> GetByTipoArticuloIdAsync(int tipoArticuloId)
        {
            var entities = await _repo.GetByTipoArticuloIdAsync(tipoArticuloId);
            return _mapper.Map<List<CampoArticuloDto>>(entities);
        }
    }
}
