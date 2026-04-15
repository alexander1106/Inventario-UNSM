using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.DTO
{
    public class UsuarioResponseDTO
    {
        public int Id { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public Boolean? Estado { get; set; }
        public int RolId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }

    }
}