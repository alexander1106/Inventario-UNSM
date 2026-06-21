using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices
{
    public interface IEscuelasService
    {
        Task<IEnumerable<Escuelas>> GetAllAsync();
        Task<Escuelas?> GetByIdAsync(int id);
        Task<IEnumerable<Escuelas>> GetByFacultadIdAsync(int facultadId);
        Task<Escuelas> CreateAsync(Escuelas escuela);
        Task<Escuelas> UpdateAsync(int id, Escuelas escuela);
        Task DeleteAsync(int id);
    }
}