using AutoMapper;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Prestamos.DTO;
using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioResponseDTO>();
        CreateMap<UsuarioUpdateDTO, Usuario>();
        CreateMap<Roles, RolesDTO>();

        CreateMap<UsuarioCreateDTO, Usuario>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombres))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellidos))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.RolId)); // << mapear RolId, NO Rol


        // ====== ROLES ======
        CreateMap<Roles, RolesDTO>();
        CreateMap<RolesDTO, Roles>();

        CreateMap<Prestamos, PrestamoDTO>();
        CreateMap<CreatePrestamoDTO, Prestamos>();
        CreateMap<UpdatePrestamosDTO, Prestamos>();

        // ====== MÓDULOS ======
        CreateMap<Modulo, ModuloDTO>();
        CreateMap<ModuloDTO, Modulo>();

        // ====== SUBMÓDULOS ======
        CreateMap<SubModulo, SubModuloDTO>();
        CreateMap<SubModuloDTO, SubModulo>();

        // ====== PERMISOS ======
        CreateMap<RolPermisos, RolPermisos>();
        CreateMap<RolPermisos, RolPermisosDTO>().ReverseMap();

        CreateMap<Ubicacion, UbicacionDto>()
            .ForMember(dest => dest.UsuarioId,
                opt => opt.MapFrom(src => src.UsuarioId));
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
