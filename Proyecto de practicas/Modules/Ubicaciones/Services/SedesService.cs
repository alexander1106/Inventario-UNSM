using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services
{
    public class SedesService : ISedesService
    {
        private readonly ISedesRepository _repository;

        public SedesService(ISedesRepository repository)
        {
            _repository = repository;
        }

        // GET NORMAL
        public async Task<IEnumerable<Sedes>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // GET NORMAL POR ID
        public async Task<Sedes?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // GET DETALLE
        public async Task<IEnumerable<SedeDetalleDto>> GetAllDetalleAsync()
        {
            return await _repository.GetAllDetalleAsync();
        }

        // GET DETALLE POR ID
        public async Task<SedeDetalleDto?> GetDetalleByIdAsync(int id)
        {
            return await _repository.GetDetalleByIdAsync(id);
        }

        public async Task<Sedes> CreateAsync(Sedes sede)
        {
            var existentes = await _repository.GetAllAsync();

            if (existentes.Any(x =>
                x.Nombre.Trim().ToUpper() ==
                sede.Nombre.Trim().ToUpper()))
            {
                throw new Exception("Ya existe una sede con ese nombre.");
            }

            return await _repository.CreateAsync(sede);
        }

        public async Task<Sedes> UpdateAsync(int id, Sedes sede)
        {
            var actual = await _repository.GetByIdAsync(id);

            if (actual == null)
                throw new Exception("La sede no existe.");

            var existentes = await _repository.GetAllAsync();

            if (existentes.Any(x =>
                x.Id != id &&
                x.Nombre.Trim().ToUpper() ==
                sede.Nombre.Trim().ToUpper()))
            {
                throw new Exception("Ya existe una sede con ese nombre.");
            }

            actual.Nombre = sede.Nombre;
            actual.Direccion = sede.Direccion;
            actual.Estado = sede.Estado;

            await _repository.UpdateAsync(actual);

            return actual;
        }

        public async Task DeleteAsync(int id)
        {
            var sede = await _repository.GetByIdAsync(id);

            if (sede == null)
                throw new Exception("La sede no existe.");

            await _repository.DeleteAsync(id);
        }
    }
}