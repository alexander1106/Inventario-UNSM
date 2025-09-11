using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Service
{
    public class PisosService : IPisosService
    {
        private readonly IPisosRepository _repository;

        public PisosService(IPisosRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Pisos>> GetListPisos()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Pisos?> GetPisos(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Pisos> AddPisos(Pisos piso)
        {
            // Lógica de negocio: numero único
            var existente = await _repository.GetByNumeroAsync(piso.Numero);
            if (existente != null)
                throw new Exception("Ya existe un piso con ese numero");

            return await _repository.AddAsync(piso);
        }


        public async Task<Pisos?> ActualizarPisoAsync(Pisos piso)
        {
            var existente = await _repository.GetByIdAsync(piso.Id);
            if (existente == null) return null;

            if (piso.Numero <= 0)
                throw new Exception("El número debe ser mayor a 0");

            existente.Numero = piso.Numero;

            return await _repository.UpdateAsync(existente);
        }

        public async Task<bool> EliminarPisoAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
