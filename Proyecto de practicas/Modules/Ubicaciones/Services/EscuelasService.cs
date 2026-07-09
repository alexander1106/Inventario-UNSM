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

        private async Task<string?> GuardarImagen(IFormFile? file)
        {
            if (file == null) return null;

            var folder = Path.Combine("wwwroot", "uploads", "escuelas");
            Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/escuelas/" + fileName;
        }

        public async Task<Escuelas> CreateAsync(Escuelas escuela, IFormFile? imagen)
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

            escuela.ImagenUrl = await GuardarImagen(imagen);

            return await _repository.CreateAsync(escuela);
        }

        public async Task<Escuelas> UpdateAsync(int id, Escuelas escuela, IFormFile? imagen)
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

            if (imagen != null)
            {
                actual.ImagenUrl = await GuardarImagen(imagen);
            }

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

        public async Task<Escuelas> AsignarUsuarioAsync(int escuelaId, int usuarioId)
        {
            return await _repository.AsignarUsuarioAsync(escuelaId, usuarioId);
        }

        public async Task<Escuelas?> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _repository.GetByUsuarioIdAsync(usuarioId);
        }

        public async Task<Escuelas> AsignarTecnicoAsync(int escuelaId, int usuarioId)
        {
            return await _repository.AsignarTecnicoAsync(escuelaId, usuarioId);
        }

        public async Task<Escuelas?> GetByTecnicoIdAsync(int usuarioId)
        {
            return await _repository.GetByTecnicoIdAsync(usuarioId);
        }
    }
}