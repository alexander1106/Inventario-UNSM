namespace Proyecto_de_practicas.Models
{
    public class Usuario
    {
        public int Id { get; set; } // EF Core por convención lo hace autoincrementable
        public string Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Correo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Estado { get; set; } = "Activo";
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación con Roles
        public int RolId { get; set; }  // FK
        public Roles Rol { get; set; }   // Navegación
    }
}
