namespace Proyecto_de_practicas.Modules.Notificaciones.Entities
{
    public class Notificacion
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public int? ArticuloId { get; set; }
        public int? PrestamoId { get; set; }
        public int UsuarioDestinoId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public bool Leido { get; set; } = false;
        public DateTime? FechaLectura { get; set; }
    }
}
