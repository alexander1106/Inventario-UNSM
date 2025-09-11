using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Service
{
    public class CategoriasService : ICategoriasService
    {
        private readonly ICategoriasRepository _repository;

        public CategoriasService(ICategoriasRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Categorias>> GetListCategorias()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Categorias?> GetCategorias(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Categorias> AddCategorias(Categorias categoria)
        {
            // Lógica de negocio: nombre único
            var existente = await _repository.GetByNombreAsync(categoria.Nombre);
            if (existente != null)
                throw new Exception("Ya existe una categoria con ese nombre");

            return await _repository.AddAsync(categoria);
        }

        public async Task<Categorias?> ActualizarCategoriaAsync(Categorias categoria)
        {
            var existente = await _repository.GetByIdAsync(categoria.Id);
            if (existente == null) return null;

            if (string.IsNullOrEmpty(categoria.Nombre))
                throw new Exception("El nombre no puede estar vacío");

            existente.Nombre = categoria.Nombre;

            return await _repository.UpdateAsync(existente);
        }

        public async Task<bool> EliminarCategoriaAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
