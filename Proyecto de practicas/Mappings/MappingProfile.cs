using AutoMapper;
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
        // ====== USUARIOS ======

        // Mapear entidad a DTO de salida (para mostrar usuario, incluyendo el nombre del rol)
        CreateMap<Usuario, UsuariosDto>()
            .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol != null ? src.Rol.Nombre : ""));

        // Mapear DTO de salida a entidad (para actualizaciones si es necesario)
        CreateMap<UsuariosDto, Usuario>();

        // Mapear DTO de creación a entidad (para crear usuario)
        CreateMap<UsuarioCreateDTO, Usuario>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombres))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellidos))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => "Activo"))
            .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.RolId)); // << mapear RolId, NO Rol


        // ====== ROLES ======
        CreateMap<Roles, RolesDTO>();
        CreateMap<RolesDTO, Roles>();

        // ====== MÓDULOS ======
        CreateMap<Modulo, ModuloDTO>();
        CreateMap<ModuloDTO, Modulo>();

        // ====== SUBMÓDULOS ======
        CreateMap<SubModulo, SubModuloDTO>();
        CreateMap<SubModuloDTO, SubModulo>();

        // ====== PERMISOS ======
        CreateMap<RolSubModulo, RolSubModuloDto>();
        CreateMap<RolSubModuloDto, RolSubModulo>();

        // ====== UBICACIONES ======
        CreateMap<Ubicacion, UbicacionDto>();
        CreateMap<UbicacionDto, Ubicacion>()
            .ForMember(dest => dest.TipoUbicacion, opt => opt.Ignore());
        

        // ====== TIPOS DE ARTÍCULO ======
        CreateMap<TipoArticulo, TipoArticuloDTO>()
            .ForMember(dest => dest.Imagen, opt => opt.Ignore())
            .ReverseMap();

        // ====== CAMPOS DE ARTÍCULO ======
        CreateMap<CampoArticulo, CampoArticuloDto>();
        CreateMap<CampoArticuloDto, CampoArticulo>();

        // ====== VALORES DE CAMPOS ======
        CreateMap<ArticuloCampoValor, ArticuloCampoValorDto>();
        CreateMap<ArticuloCampoValorDto, ArticuloCampoValor>();

        

        CreateMap<Articulo, ArticuloDto>();
        CreateMap<ArticuloDto, Articulo>();

        CreateMap<ArticuloCampoValor, ArticuloCampoValorDto>();
        CreateMap<ArticuloCampoValorDto, ArticuloCampoValor>();
    }
}
