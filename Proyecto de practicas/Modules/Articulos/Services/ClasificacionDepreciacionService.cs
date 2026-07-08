using AutoMapper;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;

namespace Proyecto_de_practicas.Modules.Articulos.Services
{
    public class ClasificacionDepreciacionService : IClasificacionDepreciacionService
    {
        private readonly IClasificacionDepreciacionRepository _repo;
        private readonly IMapper _mapper;

        public ClasificacionDepreciacionService(IClasificacionDepreciacionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ClasificacionDepreciacionDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<ClasificacionDepreciacionDto>>(entities);
        }

        public async Task<ClasificacionDepreciacionDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ClasificacionDepreciacionDto>(entity);
        }

        public async Task<ClasificacionDepreciacionDto> AddAsync(ClasificacionDepreciacionDto dto)
        {
            bool existe = await _repo.ExisteNombreAsync(dto.Nombre);
            if (existe)
                throw new InvalidOperationException("Ya existe una clasificación de depreciación con ese nombre.");

            var entity = _mapper.Map<ClasificacionDepreciacion>(dto);
            entity.PorcentajeDepreciacionAnual = entity.VidaUtilAnios > 0 ? 100.0 / entity.VidaUtilAnios : 0;

            var result = await _repo.AddAsync(entity);
            return _mapper.Map<ClasificacionDepreciacionDto>(result);
        }

        public async Task<ClasificacionDepreciacionDto> UpdateAsync(int id, ClasificacionDepreciacionDto dto)
        {
            bool existe = await _repo.ExisteNombreAsync(dto.Nombre, id);
            if (existe)
                throw new InvalidOperationException("Ya existe una clasificación de depreciación con ese nombre.");

            var entity = _mapper.Map<ClasificacionDepreciacion>(dto);
            entity.Id = id;
            entity.PorcentajeDepreciacionAnual = entity.VidaUtilAnios > 0 ? 100.0 / entity.VidaUtilAnios : 0;

            var result = await _repo.UpdateAsync(entity);
            return _mapper.Map<ClasificacionDepreciacionDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool tieneArticulos = await _repo.TieneRelacionConArticulosAsync(id);
            if (tieneArticulos)
                throw new InvalidOperationException(
                    "No se puede eliminar esta clasificación porque tiene artículos relacionados.");

            return await _repo.DeleteAsync(id);
        }
    }
}
