using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository
{
    public interface IEscuelasRepository
    {
        Task<IEnumerable<Escuelas>> GetAllAsync();
        Task<Escuelas?> GetByIdAsync(int id);
        Task<IEnumerable<Escuelas>> GetByFacultadIdAsync(int facultadId);

        Task<Escuelas> CreateAsync(Escuelas escuela);
        Task UpdateAsync(Escuelas escuela);
        Task DeleteAsync(int id);
    }
}
