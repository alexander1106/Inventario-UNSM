namespace Proyecto_de_practicas.Models
{
    public class Usuario
    {
        public int Id { get; set; } // EF Core autoincrementable
        public string Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Correo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Estado { get; set; }
        public int EstadoInt { get; set; } = 1; 
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public ICollection<UsuarioFacultadRol> UsuariosFacultadesRoles { get; set; }
        public Usuario() { }

        public Usuario(string nombre, string correo, string username, string password, string estado = "Activo", string? apellido = null)
        {
            Nombre = nombre;
            Correo = correo;
            Username = username;
            Password = password;
            Estado = estado;
            Apellido = apellido;
        }
    }
}
