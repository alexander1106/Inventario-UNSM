using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services
{
    public class FacultadesService : IFacultadesService
    {
        private readonly IFacultadesRepository _repository;
        private readonly ISedesRepository _sedesRepository;

        public FacultadesService(
            IFacultadesRepository repository,
            ISedesRepository sedesRepository)
        {
            _repository = repository;
            _sedesRepository = sedesRepository;
        }
        public async Task<IEnumerable<FacultadesDetalleDto>> GetAllDetalleAsync()
        {
            return await _repository.GetAllDetalleAsync();
        }

        public async Task<FacultadesDetalleDto?> GetDetalleByIdAsync(int id)
        {
            return await _repository.GetDetalleByIdAsync(id);
        }
        public async Task<IEnumerable<Facultades>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Facultades?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Facultades>> GetBySedeIdAsync(int sedeId)
        {
            return await _repository.GetBySedeIdAsync(sedeId);
        }

        public async Task<Facultades> CreateAsync(Facultades facultad)
        {
            var sede = await _sedesRepository.GetByIdAsync(facultad.SedeId);

            if (sede == null)
                throw new Exception("La sede especificada no existe.");

            var facultades = await _repository.GetBySedeIdAsync(facultad.SedeId);

            if (facultades.Any(x =>
                x.Nombre.Trim().ToUpper() ==
                facultad.Nombre.Trim().ToUpper()))
            {
                throw new Exception("La facultad ya existe en esta sede.");
            }

            return await _repository.CreateAsync(facultad);
        }

        public async Task<Facultades> UpdateAsync(int id, Facultades facultad)
        {
            var actual = await _repository.GetByIdAsync(id);

            if (actual == null)
                throw new Exception("La facultad no existe.");

            var facultades = await _repository.GetBySedeIdAsync(facultad.SedeId);

            if (facultades.Any(x =>
                x.Id != id &&
                x.Nombre.Trim().ToUpper() ==
                facultad.Nombre.Trim().ToUpper()))
            {
                throw new Exception("La facultad ya existe en esta sede.");
            }

            actual.Nombre = facultad.Nombre;
            actual.SedeId = facultad.SedeId;

            await _repository.UpdateAsync(actual);

            return actual;
        }

        public async Task DeleteAsync(int id)
        {
            var facultad = await _repository.GetByIdAsync(id);

            if (facultad == null)
                throw new Exception("La facultad no existe.");

            await _repository.DeleteAsync(id);
        }

    }
}