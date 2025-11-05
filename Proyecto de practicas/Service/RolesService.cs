using AutoMapper;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Repository.IRepository;

namespace Proyecto_de_practicas.Service
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _repository;
        private readonly IMapper _mapper;

        public RolesService(IRolesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteRol(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<RolesDTO>> GetAllRolesAsync()
        {
            var roles = await _repository.GetAllAsync();
            return _mapper.Map<List<RolesDTO>>(roles);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            var rol = await _repository.GetByNombreAsync(roleName);
            return rol != null;
        }

        public async Task<RolesDTO> AddRoleAsync(RolesDTO dto)
        {
            var nombreNormalizado = dto.Nombre.Trim().ToUpper();

            var existente = await _repository.GetByNombreAsync(nombreNormalizado);
            if (existente != null)
                throw new Exception("Ya existe un rol con ese nombre");

            dto.Nombre = nombreNormalizado;

            var entity = _mapper.Map<Roles>(dto);
            var saved = await _repository.AddAsync(entity);

            return _mapper.Map<RolesDTO>(saved);
        }

      
    }
}
