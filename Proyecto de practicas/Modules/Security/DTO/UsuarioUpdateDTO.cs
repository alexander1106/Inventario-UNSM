using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.DTO
{
    public class UsuarioUpdateDTO
    {
        public int Id { get; set; }
        public IFormFile? Imagen { get; set; }

        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public Boolean? Estado { get; set; }

        public int? RolId { get; set; } // ✅ SOLO ESTO
    }
}