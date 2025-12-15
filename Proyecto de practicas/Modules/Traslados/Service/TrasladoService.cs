using Proyecto_de_practicas.Modules.Traslados.Entities;
using Proyecto_de_practicas.Modules.Traslados.Repository.IRespository;
using Proyecto_de_practicas.Modules.Traslados.Service.IService;

namespace Proyecto_de_practicas.Modules.Traslados.Service
{
    public class TrasladoService : ITrasladoService
    {
        private readonly ITrasladoRepository _repository;

        public TrasladoService(ITrasladoRepository repository)
        {
            _repository = repository;
        }

        // 🔹 Obtener todos los traslados
        public Task<List<Traslado>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        // 🔹 Obtener traslado por ID
        public Task<Traslado?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        // 🔹 Crear traslado (solo registro, NO mueve artículo)
        public Task<Traslado> CreateAsync(Traslado traslado)
        {
            return _repository.CreateAsync(traslado);
        }

        // 🔹 Actualizar traslado
        public Task<Traslado> UpdateAsync(Traslado traslado)
        {
            return _repository.UpdateAsync(traslado);
        }

        // 🔹 Eliminar traslado
        public Task<bool> DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        // 🔥 MÉTODO CLAVE: realiza traslado + mueve artículo
        public Task<bool> RealizarTrasladoAsync(Traslado traslado)
        {
            return _repository.RealizarTrasladoAsync(traslado);
        }
    }
}
