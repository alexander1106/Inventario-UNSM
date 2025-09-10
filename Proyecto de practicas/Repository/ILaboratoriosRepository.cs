

using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface ILaboratoriosRepository
    {
        Task<List<Laboratorios>> GetAllAsync();
        Task<Laboratorios?> GetByIdAsync(int id);
        Task<Laboratorios?> GetByNombreAsync(string nombre);
        Task<Laboratorios> AddAsync(Laboratorios laboratorio);
        Task<Laboratorios> UpdateAsync(Laboratorios laboratorio);
        Task<bool> DeleteAsync(int id);
    }
}
