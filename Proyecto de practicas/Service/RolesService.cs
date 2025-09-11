using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;


namespace Proyecto_de_practicas.Service
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _repository;

        public RolesService(IRolesRepository repository)
        {
            _repository = repository;
        }

        public async Task AddRoleAsync(string roleName)
        {
            if (!await RoleExistsAsync(roleName))
            {
                var rol = new Roles { Nombre = roleName };
                await _repository.AddAsync(rol);
            }
        }

        public async Task DeleteRoleAsync(string roleName)
        {
            var rol = await _repository.GetByNombreAsync(roleName);
            if (rol != null)
            {
                await _repository.DeleteAsync(rol.Id);
            }
        }

        public async Task<List<string>> GetAllRolesAsync()
        {
            var roles = await _repository.GetAllAsync();
            return roles.Select(r => r.Nombre).ToList();
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            var rol = await _repository.GetByNombreAsync(roleName);
            return rol != null;
        }
    }
}