using Proyecto_de_practicas.Modules.Security.DTO;

namespace Proyecto_de_practicas.Modules.Security.Services.IServices
{
    public interface IRolPermisosService
    {
        Task<RolPermisosDTO?> GetAsync(int rolId, int subModuloId, int permisoId);
        Task<List<SubModuloDTO>> GetByRolAsync(int rolId);
        Task<RolPermisosDTO> CreateAsync(RolPermisosDTO dto);

        // 🔥 NUEVOS
        Task<RolPermisosDTO?> UpdateByIdAsync(int id, RolPermisosDTO dto);
        Task SyncPermisosAsync(int rolId, List<RolPermisosDTO> permisos);

        Task<bool> DeleteAsync(int rolId, int subModuloId, int permisoId);
        Task<List<ModuloConSubModulosDto>> GetModulosConSubModulosPorRolAsync(int rolId);
        Task<RolAccesoDTO> GetAccesosPorRolAsync(int rolId);    
    }
}