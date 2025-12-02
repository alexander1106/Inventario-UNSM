using AutoMapper;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // USUARIOS
        CreateMap<Usuario, UsuariosDto>();
        CreateMap<UsuariosDto, Usuario>();

        // ROLES
        CreateMap<Roles, RolesDTO>();
        CreateMap<RolesDTO, Roles>();

        // MÓDULOS
        CreateMap<Modulo, ModuloDTO>();
        CreateMap<ModuloDTO, Modulo>();

        // SUBMÓDULOS
        CreateMap<SubModulo, SubModuloDTO>();
        CreateMap<SubModuloDTO, SubModulo>();

        // PERMISOS POR SUBMÓDULO
        CreateMap<RolSubModulo, RolSubModuloDto>();
        CreateMap<RolSubModuloDto, RolSubModulo>();

        // CREAR USUARIO
        CreateMap<UsuarioCreateDTO, Usuario>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombres))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellidos))
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => "Activo"));

        CreateMap<Usuario, UsuariosDto>();
        CreateMap<UsuariosDto, Usuario>();

        // 🏢 UBICACIÓN
        CreateMap<UbicacionDto, Ubicacion>()
            .ForMember(dest => dest.TipoUbicacion, opt => opt.Ignore()); // evita errores

        CreateMap<Ubicacion, UbicacionDto>();

        CreateMap<TipoArticuloDTO, TipoArticulo>().ReverseMap();
        CreateMap<CampoArticuloDto, CampoArticulo>();
        CreateMap<CampoArticulo, CampoArticuloDto>();
        CreateMap<ArticuloDto, Articulo>();
        CreateMap<Articulo, ArticuloDto>();
    }
}

