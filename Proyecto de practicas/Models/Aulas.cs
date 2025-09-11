namespace Proyecto_de_practicas.Models
{
    public class Aulas
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? Piso { get; set; }
        public string Estado { get; set; } = "Activo"; // Borrado lógico

        // Relación con Herramientas
        public ICollection<Herramientas> Herramientas { get; set; } = new List<Herramientas>();
    }
}
