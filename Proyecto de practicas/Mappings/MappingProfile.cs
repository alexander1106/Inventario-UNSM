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

        CreateMap<CreatePrestamoDTO, Prestamos>();
        CreateMap<UpdatePrestamosDTO, Prestamos>();
        CreateMap<Prestamos, PrestamoDTO>();
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

        

        CreateMap<Articulo, ArticuloDto>()
            .ForMember(dest => dest.Id,               opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CodigoPatrimonial, opt => opt.MapFrom(src => src.CodigoPatrimonial))
            .ForMember(dest => dest.CodigoBarra,       opt => opt.MapFrom(src => src.CodigoBarra))
            .ForMember(dest => dest.Nombre,            opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.FechaAdquision,    opt => opt.MapFrom(src => src.FechaAdquision))
            .ForMember(dest => dest.ValorAdquisitivo,  opt => opt.MapFrom(src => src.ValorAdquisitivo))
            .ForMember(dest => dest.Condicion,         opt => opt.MapFrom(src => src.Condicion))
            .ForMember(dest => dest.TipoArticuloId,    opt => opt.MapFrom(src => src.TipoArticuloId))
            .ForMember(dest => dest.UbicacionId,       opt => opt.MapFrom(src => src.UbicacionId))
            .ForMember(dest => dest.Marca,             opt => opt.MapFrom(src => src.Marca))
            .ForMember(dest => dest.Modelo,            opt => opt.MapFrom(src => src.Modelo))
            .ForMember(dest => dest.NroSerie,          opt => opt.MapFrom(src => src.NroSerie))
            .ForMember(dest => dest.Medidas,           opt => opt.MapFrom(src => src.Medidas))
            .ForMember(dest => dest.Color,             opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.TiempoVidaUtil,    opt => opt.MapFrom(src => src.TiempoVidaUtil))
            .ForMember(dest => dest.DepreciacionAnual, opt => opt.MapFrom(src => src.DepreciacionAnual))
            .ForMember(dest => dest.ValorActual,       opt => opt.MapFrom(src => src.ValorActual))
            .ForMember(dest => dest.Estado,            opt => opt.MapFrom(src => src.Estado));

        CreateMap<ArticuloDto, Articulo>()
            .ForMember(dest => dest.TipoArticulo, opt => opt.Ignore())
            .ForMember(dest => dest.Ubicacion,    opt => opt.Ignore())
            .ForMember(dest => dest.Prestamos,    opt => opt.Ignore());

        CreateMap<ArticuloCampoValor, ArticuloCampoValorDto>();
        CreateMap<ArticuloCampoValorDto, ArticuloCampoValor>();

    }
}
