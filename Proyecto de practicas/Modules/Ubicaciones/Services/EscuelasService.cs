using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Services
{
    public class EscuelasService : IEscuelasService
    {
        private readonly IEscuelasRepository _repository;
        private readonly IFacultadesRepository _facultadesRepository;

        public EscuelasService(
            IEscuelasRepository repository,
            IFacultadesRepository facultadesRepository)
        {
            _repository = repository;
            _facultadesRepository = facultadesRepository;
        }

        public async Task<IEnumerable<Escuelas>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Escuelas?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Escuelas>> GetByFacultadIdAsync(int facultadId)
        {
            return await _repository.GetByFacultadIdAsync(facultadId);
        }

        public async Task<Escuelas> CreateAsync(Escuelas escuela)
        {
            var facultad = await _facultadesRepository.GetByIdAsync(escuela.FacultadId);

            if (facultad == null)
                throw new Exception("La facultad no existe.");

            var escuelas = await _repository.GetByFacultadIdAsync(escuela.FacultadId);

            if (escuelas.Any(x =>
                x.Nombre.Trim().ToUpper() ==
                escuela.Nombre.Trim().ToUpper()))
            {
                throw new Exception("La escuela ya existe en esta facultad.");
            }

            return await _repository.CreateAsync(escuela);
        }

        public async Task<Escuelas> UpdateAsync(int id, Escuelas escuela)
        {
            var actual = await _repository.GetByIdAsync(id);

            if (actual == null)
                throw new Exception("La escuela no existe.");

            var escuelas = await _repository.GetByFacultadIdAsync(escuela.FacultadId);

            if (escuelas.Any(x =>
                x.Id != id &&
                x.Nombre.Trim().ToUpper() ==
                escuela.Nombre.Trim().ToUpper()))
            {
                throw new Exception("La escuela ya existe en esta facultad.");
            }

            actual.Nombre = escuela.Nombre;
            actual.FacultadId = escuela.FacultadId;

            await _repository.UpdateAsync(actual);

            return actual;
        }

        public async Task DeleteAsync(int id)
        {
            var escuela = await _repository.GetByIdAsync(id);

            if (escuela == null)
                throw new Exception("La escuela no existe.");

            await _repository.DeleteAsync(id);
        }
    }
}