namespace Proyecto_de_practicas.Modules.Security.DTO
{
    public class UsuarioCreateDTO
    {
        public string Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
    }
}
