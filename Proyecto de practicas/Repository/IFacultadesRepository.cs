using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface IFacultadesRepository
    {
        Task<List<Facultades>> GetAllAsync();
        Task<Facultades?> GetByIdAsync(int id);
        Task<Facultades?> GetByNombreAsync(string nombre);
        Task<Facultades> AddAsync(Facultades facultad);
        Task<Facultades> UpdateAsync(Facultades facultad);
        Task<bool> DeleteAsync(int id);
    }
}
