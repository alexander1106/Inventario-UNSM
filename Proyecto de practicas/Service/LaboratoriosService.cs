


using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Service
{
    public class LaboratoriosService : ILaboratoriosService
    {
        private readonly ILaboratoriosRepository _repository;

        public LaboratoriosService(ILaboratoriosRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Laboratorios>> GetListLaboratorios()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Laboratorios?> GetLaboratorios(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Laboratorios> AddLaboratorios(Laboratorios laboratorio)
        {
            // Lógica de negocio: nombre único
            var existente = await _repository.GetByNombreAsync(laboratorio.Nombre);
            if (existente != null)
                throw new Exception("Ya existe un laboratorio con ese nombre");

            return await _repository.AddAsync(laboratorio);
        }

        public async Task<Laboratorios?> ActualizarLaboratorioAsync(Laboratorios laboratorio)
        {
            var existente = await _repository.GetByIdAsync(laboratorio.Id);
            if (existente == null) return null;

            if (string.IsNullOrEmpty(laboratorio.Nombre))
                throw new Exception("El nombre no puede estar vacío");

            existente.Nombre = laboratorio.Nombre;
            existente.Piso = laboratorio.Piso;

            return await _repository.UpdateAsync(existente);
        }

        public async Task<bool> EliminarLaboratorioAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}