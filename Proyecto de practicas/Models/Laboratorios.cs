namespace Proyecto_de_practicas.Models
{
    public class Laboratorios
    {
        public int Id { get; set; } // Autoincrementable
        public string Nombre { get; set; } = string.Empty;
        public int Piso { get; set; }
        public string Estado { get; set; } = "Activo"; // Borrado lógico

        // Relación con Herramientas
        public ICollection<Herramientas> Herramientas { get; set; } = new List<Herramientas>();
    }
}