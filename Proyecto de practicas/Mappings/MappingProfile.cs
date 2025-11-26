using AutoMapper;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;

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
        CreateMap<RolSubModulo, RolSubModuloDto>();
        CreateMap<RolSubModuloDto, RolSubModulo>();

        CreateMap<SubModulo, SubModuloDTO>();
        CreateMap<Modulo, ModuloDTO>();
        CreateMap<RolSubModulo, RolSubModuloDto>();

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


        // De entidad a DTO
        CreateMap<TipoArticulo, TipoArticuloDTO>()
            .ForMember(dest => dest.Imagen, opt => opt.Ignore());

        // De DTO a entidad
        CreateMap<TipoArticuloDTO, TipoArticulo>()
            .ForMember(dest => dest.Imagen, opt => opt.Ignore())
            .ForMember(dest => dest.ImagenPath, opt => opt.MapFrom(src => src.ImagenPath));
    }
}

