using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface ICategoriasRepository
    {
        Task<List<Categorias>> GetAllAsync();
        Task<Categorias?> GetByIdAsync(int id);
        Task<Categorias?> GetByNombreAsync(string nombre);
        Task<Categorias> AddAsync(Categorias categoria);
        Task<Categorias> UpdateAsync(Categorias categoria);
        Task<bool> DeleteAsync(int id);
    }
}
