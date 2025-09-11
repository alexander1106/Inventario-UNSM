using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface ICategoriasService
    {
        Task<List<Categorias>> GetListCategorias();
        Task<Categorias?> GetCategorias(int id);
        Task<Categorias> AddCategorias(Categorias categoria);
        Task<Categorias?> ActualizarCategoriaAsync(Categorias categoria);
        Task<bool> EliminarCategoriaAsync(int id);
    }
}
