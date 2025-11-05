using AutoMapper;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository.IRepository;

namespace Proyecto_de_practicas.Service
{
    public class UsuarioFacultadRolService : IUsuarioFacultadRolService
    {
        private readonly IUsuarioFacultadRolRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioFacultadRolService(IUsuarioFacultadRolRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Servicio
        public async Task<string> AsignarUsuarioFacultadRolAsync(UsuarioFacultadRolDTO dto)
        {
            var existe = await _repository.ExisteAsignacionAsync(dto.IdUsuario, dto.IdFacultad, dto.IdRol);
            if (existe)
                return "La asignación ya existe.";

            var entidad = _mapper.Map<UsuarioFacultadRol>(dto);
            await _repository.AgregarAsync(entidad);
            await _repository.GuardarAsync();

            return "Asignación realizada correctamente.";
        }



    }
}