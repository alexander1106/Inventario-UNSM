namespace Proyecto_de_practicas.DTO
{
    public class UsuariosDto
    {
        public string Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Correo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Estado { get; set; } = "Activo";
        public int EstadoInt { get; set; } = 1;
        public UsuariosDto()
        {
        }

        public UsuariosDto(string nombre, string? apellido, string correo, string username, string password, string estado = "Activo", int estadoInt = 1)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Username = username;
            Password = password;
            Estado = estado;
            EstadoInt = estadoInt;
        }




    }
}