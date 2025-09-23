namespace Proyecto_de_practicas.DTO
{
    public class ArticuloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        // Información del tipo de artículo
        public int TipoArticuloId { get; set; }


        // Información de ubicación
        public int? UbicacionId { get; set; }

        // Estado del artículo (1 = activo, 0 = borrado lógico)
        public int Estado { get; set; } = 1;
    }
}