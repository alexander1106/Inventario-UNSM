using AutoMapper;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Services.IServices;

namespace Proyecto_de_practicas.Modules.Security.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public RolesService(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<List<RolesDTO>> GetAllRolesAsync()
        {
            var roles = await _rolesRepository.GetAllAsync();
            return _mapper.Map<List<RolesDTO>>(roles);
        }

        public async Task<RolesDTO?> GetByIdAsync(int id)
        {
            var role = await _rolesRepository.GetByIdAsync(id);
            return role == null ? null : _mapper.Map<RolesDTO>(role);
        }

        public async Task<RolesDTO?> GetByNombreAsync(string nombre)
        {
            var role = await _rolesRepository.GetByNombreAsync(nombre);
            return role == null ? null : _mapper.Map<RolesDTO>(role);
        }

        public async Task<bool> RoleExistsAsync(string nombre)
        {
            var role = await _rolesRepository.GetByNombreAsync(nombre);
            return role != null;
        }

        public async Task<RolesDTO> AddRoleAsync(RolesDTO rol)
        {
            if (await RoleExistsAsync(rol.Nombre))
                throw new Exception("El rol ya existe.");

            var entity = _mapper.Map<Roles>(rol);
            var creado = await _rolesRepository.AddAsync(entity);
            return _mapper.Map<RolesDTO>(creado);
        }

        public async Task<RolesDTO> UpdateRoleAsync(RolesDTO rol)
        {
            var entity = _mapper.Map<Roles>(rol);
            var actualizado = await _rolesRepository.UpdateAsync(entity);

            if (actualizado == null)
                throw new Exception("No se encontró el rol para actualizar.");

            return _mapper.Map<RolesDTO>(actualizado);
        }

        public async Task<bool> DeleteRol(int id)
        {
            return await _rolesRepository.DeleteAsync(id);
        }
    }
}