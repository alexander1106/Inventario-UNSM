using Proyecto_de_practicas.DTOs;

namespace Proyecto_de_practicas.DTO
{
    public class ArticuloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int TipoArticuloId { get; set; }
        public int? UbicacionId { get; set; }
        public int Estado { get; set; } = 1;

        // 🔹 Nueva propiedad
        public UbicacionDto? Ubicacion { get; set; }
    }
}