using AutoMapper;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository.IRepository;

namespace Proyecto_de_practicas.Service
{
    public class FacultadesService : IFacultadesService
    {
        private readonly IFacultadesRepository _repository;
        private readonly IMapper _mapper;

        public FacultadesService(IFacultadesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FacultadesDto>> GetListFacultades()
        {
            var facs = await _repository.GetAllAsync();
            return _mapper.Map<List<FacultadesDto>>(facs);
        }

        public async Task<FacultadesDto?> GetFacultades(int id)
        {
            var fac = await _repository.GetByIdAsync(id);
            return _mapper.Map<FacultadesDto?>(fac);
        }

        public async Task<FacultadesDto> AddFacultades(FacultadesDto facultadDto)
        {
            var existente = await _repository.GetByNombreAsync(facultadDto.Nombre);
            if (existente != null)
                throw new Exception("Ya existe una facultad con ese nombre");

            var entity = _mapper.Map<Facultades>(facultadDto);
            var nuevo = await _repository.AddAsync(entity);

            return _mapper.Map<FacultadesDto>(nuevo);
        }

        public async Task<FacultadesDto?> ActualizarFacultadAsync(FacultadesDto facultadDto)
        {
            var existente = await _repository.GetByIdAsync(facultadDto.Id);
            if (existente == null) return null;

            if (string.IsNullOrEmpty(facultadDto.Nombre))
                throw new Exception("El nombre no puede estar vacío");

            existente.Nombre = facultadDto.Nombre;
            var actualizado = await _repository.UpdateAsync(existente);

            return _mapper.Map<FacultadesDto>(actualizado);
        }

        public async Task<bool> EliminarFacultadAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
