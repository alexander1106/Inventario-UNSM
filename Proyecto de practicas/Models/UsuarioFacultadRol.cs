using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_de_practicas.Models
{
    public class UsuarioFacultadRol
    {
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Facultad")]
        public int IdFacultad { get; set; }
        public Facultades Facultad { get; set; }

        [ForeignKey("Rol")]
        public int IdRol { get; set; }
        public Roles Rol { get; set; }
    }
}