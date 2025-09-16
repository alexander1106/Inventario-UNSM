using Proyecto_de_practicas.DTO;

namespace Proyecto_de_practicas.Service
{
    public interface IUsuarioFacultadRolService
    {
        Task<string> AsignarUsuarioFacultadRolAsync(UsuarioFacultadRolDTO dto);
    }
} 
