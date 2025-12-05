namespace Proyecto_de_practicas.Modules.Security.DTO
{
    public class UsuariosDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Estado { get; set; }
        public int? RolId { get; set; }
        public string? Password { get; set; }
    }
}
