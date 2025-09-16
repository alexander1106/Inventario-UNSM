using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Repository
{
    public interface IUsuarioFacultadRolRepository
    {
        Task<bool> ExisteAsignacionAsync(int idUsuario, int idFacultad, int idRol);
        Task AgregarAsync(UsuarioFacultadRol entidad);
        Task GuardarAsync();
    }
}
