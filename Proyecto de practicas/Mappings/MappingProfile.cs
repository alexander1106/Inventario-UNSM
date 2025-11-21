using AutoMapper;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Security;

public class MappingProfile : Profile
{   
    public MappingProfile()
    {
        // 🧍 USUARIOS
        CreateMap<Usuario, UsuariosDto>();
        CreateMap<UsuariosDto, Usuario>(); 

        // 👥 ROLES
        CreateMap<Roles, RolesDTO>();
        CreateMap<RolesDTO, Roles>();

        // 🧩 MÓDULOS
        CreateMap<Modulo, ModuloDTO>();
        CreateMap<ModuloDTO, Modulo>();

        // 🧱 SUBMÓDULOS
        CreateMap<SubModulo, SubModuloDTO>();
        CreateMap<SubModuloDTO, SubModulo>();

        // 🎯 PERMISOS POR SUBMÓDULO
        CreateMap<RolSubModulo, RolSubModuloDTO>();
        CreateMap<RolSubModuloDTO, RolSubModulo>();

        CreateMap<SubModulo, SubModuloDTO>();
        CreateMap<Modulo, ModuloDTO>();

        {
            // Crear usuario
            CreateMap<UsuarioCreateDTO, Usuario>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => "Activo"));

            // Usuario → UsuariosDto
            CreateMap<Usuario, UsuariosDto>();
                

            // UsuariosDto → Usuario
            CreateMap<UsuariosDto, Usuario>();
        }
    }
}
