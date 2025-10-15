using AutoMapper;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;

namespace Proyecto_de_practicas.Service
{
  public class ArticuloCampoValorService : IArticuloCampoValorService
    {
        private readonly IArticuloCampoValorRepository _repository;
        private readonly IMapper _mapper;

        public ArticuloCampoValorService(IArticuloCampoValorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticuloCampoValorDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ArticuloCampoValorDto>>(entities);
        }

        public async Task<ArticuloCampoValorDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ArticuloCampoValorDto>(entity);
        }

        public async Task<IEnumerable<ArticuloCampoValorDto>> GetByArticuloIdAsync(int articuloId)
        {
            var entities = await _repository.GetByArticuloIdAsync(articuloId);
            return _mapper.Map<IEnumerable<ArticuloCampoValorDto>>(entities);
        }
        
        public async Task<IEnumerable<ArticuloCampoValorDto>> GetByTipoArticuloIdAsync(int tipoArticuloId)
        {
            var entities = await _repository.GetByTipoArticuloIdAsync(tipoArticuloId);
            return _mapper.Map<IEnumerable<ArticuloCampoValorDto>>(entities);
        }


        public async Task AddAsync(ArticuloCampoValorDto dto)
        {
            var entity = _mapper.Map<ArticuloCampoValor>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(ArticuloCampoValorDto dto)
        {
            var entity = _mapper.Map<ArticuloCampoValor>(dto);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }


    }
}