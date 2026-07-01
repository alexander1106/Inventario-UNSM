using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices
{
    public interface IEscuelasService
    {
        Task<IEnumerable<Escuelas>> GetAllAsync();
        Task<Escuelas?> GetByIdAsync(int id);
        Task<IEnumerable<Escuelas>> GetByFacultadIdAsync(int facultadId);
        Task<Escuelas> CreateAsync(Escuelas escuela, IFormFile? imagen);
        Task<Escuelas> UpdateAsync(int id, Escuelas escuela, IFormFile? imagen);
        Task DeleteAsync(int id);
        Task<Escuelas> AsignarUsuarioAsync(int escuelaId, int usuarioId);
        Task<Escuelas?> GetByUsuarioIdAsync(int usuarioId);
    }
}