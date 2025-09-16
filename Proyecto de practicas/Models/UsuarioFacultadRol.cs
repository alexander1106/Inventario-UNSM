namespace Proyecto_de_practicas.Models
{
    public class UsuarioFacultadRol
    {

        public int IdUsuario { get; set; }
        public Usuario Usuarios { get; set; }

        public int IdFacultad { get; set; }
        public Facultades Facultades { get; set; }

        public int IdRol { get; set; }
        public Roles Rol { get; set; }
    }
}
