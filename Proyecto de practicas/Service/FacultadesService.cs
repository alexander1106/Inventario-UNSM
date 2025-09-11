using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Service
{
    public class FacultadesService : IFacultadesService
    {
        private readonly IFacultadesRepository _repository;

        public FacultadesService(IFacultadesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Facultades>> GetListFacultades()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Facultades?> GetFacultades(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Facultades> AddFacultades(Facultades facultad)
        {
            // Lógica de negocio: nombre único
            var existente = await _repository.GetByNombreAsync(facultad.Nombre);
            if (existente != null)
                throw new Exception("Ya existe una facultad con ese nombre");

            return await _repository.AddAsync(facultad);
        }

        public async Task<Facultades?> ActualizarFacultadAsync(Facultades facultad)
        {
            var existente = await _repository.GetByIdAsync(facultad.Id);
            if (existente == null) return null;

            if (string.IsNullOrEmpty(facultad.Nombre))
                throw new Exception("El nombre no puede estar vacío");

            existente.Nombre = facultad.Nombre;

            return await _repository.UpdateAsync(existente);
        }

        public async Task<bool> EliminarFacultadAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
