using AutoMapper;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Modules.Articulos.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _repo;
        private readonly IMapper _mapper;

        public ArticuloService(IArticuloRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ArticuloDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<ArticuloDto>>(entities);
        }

        public async Task<ArticuloDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ArticuloDto>(entity);
        }

        public async Task<ArticuloDto> AddAsync(ArticuloDto dto)
        {
            var entity = _mapper.Map<Articulo>(dto);
            var result = await _repo.AddAsync(entity);
            return _mapper.Map<ArticuloDto>(result);
        }

        public async Task<ArticuloDto> UpdateAsync(int id, ArticuloDto dto)
        {
            var entity = _mapper.Map<Articulo>(dto);
            entity.Id = id;
            var result = await _repo.UpdateAsync(entity);
            return _mapper.Map<ArticuloDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<ArticuloDto>> GetByTipoArticuloIdAsync(int tipoArticuloId)
        {
            var entities = await _repo.GetByTipoArticuloIdAsync(tipoArticuloId);
            return _mapper.Map<List<ArticuloDto>>(entities);
        }

        public async Task<List<ArticuloDto>> GetByUbicacionIdAsync(int ubicacionId)
        {
            var entities = await _repo.GetByUbicacionIdAsync(ubicacionId);
            return _mapper.Map<List<ArticuloDto>>(entities);
        }
    }
}