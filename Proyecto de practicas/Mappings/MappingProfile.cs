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


    }
}
