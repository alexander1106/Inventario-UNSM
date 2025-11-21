using AutoMapper;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

public class ModulosService : IModulosService
{
    private readonly IModulosRepository _repository;
    private readonly IMapper _mapper;

    public ModulosService(IModulosRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ModuloDTO>> GetAllAsync()
    {
        var modulos = await _repository.GetAllModulosAsync();
        return _mapper.Map<IEnumerable<ModuloDTO>>(modulos);
    }

    public async Task<ModuloDTO?> GetByIdAsync(int id)
    {
        var modulo = await _repository.GetModuloByIdAsync(id);
        return _mapper.Map<ModuloDTO>(modulo);
    }

    public async Task<IEnumerable<ModuloDTO>> SearchByNombreAsync(string nombre)
    {
        var modulos = await _repository.SearchModulosByNombreAsync(nombre);
        return _mapper.Map<IEnumerable<ModuloDTO>>(modulos);
    }
}
