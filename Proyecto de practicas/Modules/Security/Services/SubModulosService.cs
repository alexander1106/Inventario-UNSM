using AutoMapper;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

public class SubModulosService : ISubModulosService
{
    private readonly ISubModulosRepository _repository;
    private readonly IMapper _mapper;

    public SubModulosService(ISubModulosRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SubModuloDTO>> GetAllAsync()
    {
        var data = await _repository.GetAllSubModulosAsync();
        return _mapper.Map<IEnumerable<SubModuloDTO>>(data);
    }

    public async Task<SubModuloDTO> GetByIdAsync(int id)
    {
        var entity = await _repository.GetSubModuloByIdAsync(id);
        return _mapper.Map<SubModuloDTO>(entity);
    }

    public async Task<IEnumerable<SubModuloDTO>> GetByModuloIdAsync(int moduloId)
    {
        var data = await _repository.GetSubModulosByModuloIdAsync(moduloId);
        return _mapper.Map<IEnumerable<SubModuloDTO>>(data);
    }

    public async Task<IEnumerable<SubModuloDTO>> SearchByNombreAsync(string nombre)
    {
        var data = await _repository.SearchSubModulosByNombreAsync(nombre);
        return _mapper.Map<IEnumerable<SubModuloDTO>>(data);
    }
}
