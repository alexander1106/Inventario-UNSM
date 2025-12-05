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
        // ====== USUARIOS ======
        CreateMap<Usuario, UsuariosDto>();
        CreateMap<UsuariosDto, Usuario>();
        CreateMap<UsuarioCreateDTO, Usuario>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombres))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellidos))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => "Activo"));

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
        // Crear usuario
        CreateMap<UsuarioCreateDTO, Usuario>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => "Activo"));

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

        // ====== ARTÍCULOS ======
        CreateMap<Articulo, ArticuloDto>()
            .ForMember(dest => dest.CamposValores, opt => opt.MapFrom(src => src.CamposValores));
        CreateMap<ArticuloDto, Articulo>()
            .ForMember(dest => dest.CamposValores, opt => opt.MapFrom(src => src.CamposValores));

        CreateMap<Articulo, ArticuloDto>();
        CreateMap<ArticuloDto, Articulo>();

        CreateMap<ArticuloCampoValor, ArticuloCampoValorDto>();
        CreateMap<ArticuloCampoValorDto, ArticuloCampoValor>();
    }
}
