using AutoMapper;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Repository.IRepository;

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

        public async Task<List<CampoArticuloDto>> GetByTipoArticuloIdAsync(int tipoArticuloId)
        {
            var entities = await _repo.GetByTipoArticuloIdAsync(tipoArticuloId);
            return _mapper.Map<List<CampoArticuloDto>>(entities);
        }

        public async Task<CampoArticuloDto> AddAsync(CampoArticuloDto dto)
        {
            // Validar duplicado
            var existe = await _repo.ExistsDuplicateAsync(dto.NombreCampo, dto.TipoArticuloId);
            if (existe)
                throw new InvalidOperationException("Ya existe un campo con el mismo nombre para este tipo de artículo.");

            var entity = _mapper.Map<CampoArticulo>(dto);
            var result = await _repo.AddAsync(entity);
            return _mapper.Map<CampoArticuloDto>(result);
        }

        public async Task<CampoArticuloDto> UpdateAsync(int id, CampoArticuloDto dto)
        {
            // Validar relación
            var tieneRelacion = await _repo.HasRelationsAsync(id);
            if (tieneRelacion)
                throw new InvalidOperationException("No se puede editar este campo porque está relacionado con otros registros.");

            // Validar duplicado (excluyendo el actual)
            var existe = await _repo.ExistsDuplicateAsync(dto.NombreCampo, dto.TipoArticuloId, id);
            if (existe)
                throw new InvalidOperationException("Ya existe un campo con el mismo nombre para este tipo de artículo.");

            var entity = _mapper.Map<CampoArticulo>(dto);
            entity.Id = id;
            var result = await _repo.UpdateAsync(entity);
            return _mapper.Map<CampoArticuloDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tieneRelacion = await _repo.HasRelationsAsync(id);
            if (tieneRelacion)
                throw new InvalidOperationException("No se puede eliminar este campo porque está relacionado con otros registros.");

            return await _repo.DeleteAsync(id);
        }


    }
}
