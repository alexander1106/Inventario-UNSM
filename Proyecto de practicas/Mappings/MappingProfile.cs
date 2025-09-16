using AutoMapper;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.Models;
using SistemaInventario.DTO;

namespace Proyecto_de_practicas.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuariosDto>();
            CreateMap<UsuariosDto, Usuario>();

            CreateMap<Laboratorios, LaboratoriosDto>();
            CreateMap<LaboratoriosDto, Laboratorios>();

            CreateMap<Facultades, FacultadesDto>();
            CreateMap<FacultadesDto, Facultades>();

            CreateMap<Roles, RolesDTO>();
            CreateMap<RolesDTO, Roles>();

            CreateMap<Pisos, PisosDto>();
            CreateMap<PisosDto, Pisos>();

            CreateMap<Usuario, UsuariosDto>();
            CreateMap<UsuariosDto, Usuario>();

            CreateMap<Laboratorios, LaboratoriosDto>();
            CreateMap<LaboratoriosDto, Laboratorios>();

            CreateMap<UsuarioFacultadRol, UsuarioFacultadRolDTO>();
            CreateMap<UsuarioFacultadRolDTO, UsuarioFacultadRol>();
        }
    }
}
