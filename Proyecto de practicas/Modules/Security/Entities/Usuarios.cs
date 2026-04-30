using System.ComponentModel.DataAnnotations.Schema;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Security.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Boolean Estado { get; set; }
        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Roles Rol { get; set; }

        [NotMapped]
        public IFormFile? Imagen { get; set; }

        public string? ImagenPath { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        // 🔥 RELACIÓN 1 A 1
        public virtual Ubicacion Ubicacion { get; set; }
    }
}
