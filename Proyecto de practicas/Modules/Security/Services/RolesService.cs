using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
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
        private readonly AplicationDBContext _context;

        public RolesService(
          IRolesRepository rolesRepository,
          IMapper mapper,
          AplicationDBContext context)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _context = context;
        }
        public async Task<RolAccesoDTO> GetAccesosPorRolAsync(int rolId)
        {
            var data = await _context.RolPermisos
                .Where(rp => rp.RolId == rolId)
                .Include(rp => rp.SubModulo)
                    .ThenInclude(sm => sm.Modulo)
                .Include(rp => rp.Modulo) // 👈 CLAVE
                .Include(rp => rp.Permiso)
                .ToListAsync();

            var resultado = new RolAccesoDTO
            {
                RolId = rolId,
                Modulos = data
                    // 🔥 AGRUPAR DEPENDIENDO SI TIENE SUBMODULO O NO
                    .GroupBy(rp => rp.SubModulo != null
                        ? rp.SubModulo.Modulo
                        : rp.Modulo)
                    .Select(mod => new ModuloDTO
                    {
                        Id = mod.Key.Id,
                        Nombre = mod.Key.Nombre,
                        Icon = mod.Key.Icon,

                        SubModulos = mod
                            .Where(rp => rp.SubModulo != null) // 👈 SOLO LOS QUE TIENEN SUBMODULO
                            .GroupBy(rp => rp.SubModulo)
                            .Select(sub => new SubModuloDTO
                            {
                                Id = sub.Key.Id,
                                Nombre = sub.Key.Nombre,
                                Ruta = sub.Key.Ruta,
                                Icon = sub.Key.Icon,
                                ModuloId = sub.Key.ModuloId,

                                Permisos = sub
                                    .Select(rp => rp.Permiso.Nombre)
                                    .Distinct()
                                    .ToList()
                            })
                            .ToList(),

                    })
                    .ToList()
            };

            return resultado;
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
            var existente = await _context.Roles
                .FirstOrDefaultAsync(r => r.Nombre.ToLower() == rol.Nombre.ToLower());

            if (existente != null)
            {
                if (existente.Estado == 1)
                    throw new Exception("El rol ya existe.");

                // 🔥 Reactivar en vez de crear nuevo
                existente.Estado = 1;
                await _context.SaveChangesAsync();

                return _mapper.Map<RolesDTO>(existente);
            }

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
            var usuariosConRol = await _context.Usuarios
                .AnyAsync(u => u.RolId == id);

            if (usuariosConRol)
                throw new Exception("No se puede eliminar el rol porque está asignado a usuarios");

            var tienePermisos = await _context.RolPermisos
                .AnyAsync(rp => rp.RolId == id);

            if (tienePermisos)
                throw new Exception("No se puede eliminar el rol porque tiene permisos asignados");

            return await _rolesRepository.DeleteAsync(id);
        }
    }
}