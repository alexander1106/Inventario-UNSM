using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_de_practicas.Modules.Security.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Correo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Estado { get; set; }

        public int RolId { get; set; }
        public Roles Rol { get; set; }
    }
}