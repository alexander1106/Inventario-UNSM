using AutoMapper;
using Proyecto_de_practicas.DTO;
using Proyecto_de_practicas.DTOs;
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

            CreateMap<Facultades, FacultadesDto>();
            CreateMap<FacultadesDto, Facultades>();

            CreateMap<Roles, RolesDTO>();
            CreateMap<RolesDTO, Roles>();


            CreateMap<UsuarioFacultadRol, UsuarioFacultadRolDTO>();
            CreateMap<UsuarioFacultadRolDTO, UsuarioFacultadRol>();

            CreateMap<TipoArticulo, TipoArticuloDTO>();
            CreateMap<TipoArticuloDTO, TipoArticulo>()
                .ForMember(dest => dest.Articulos, opt => opt.Ignore());
            CreateMap<Articulo, ArticuloDto>();
            CreateMap<ArticuloDto, Articulo>();

            CreateMap<Ubicacion, UbicacionDto>();

            CreateMap<UbicacionDto, Ubicacion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // 👈 clave


            CreateMap<CampoArticulo, CampoArticuloDto>();
            CreateMap<CampoArticuloDto, CampoArticulo>();

            CreateMap<ArticuloCampoValor, ArticuloCampoValorDto>();
            CreateMap<ArticuloCampoValorDto, ArticuloCampoValor>();


        }
    }
}
