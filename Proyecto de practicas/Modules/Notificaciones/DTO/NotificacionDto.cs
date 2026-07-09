namespace Proyecto_de_practicas.Modules.Notificaciones.DTO
{
    public class NotificacionDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public int? ArticuloId { get; set; }
        public int? PrestamoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Leido { get; set; }
        public DateTime? FechaLectura { get; set; }
    }
}
