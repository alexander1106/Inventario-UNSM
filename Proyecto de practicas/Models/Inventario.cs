namespace Proyecto_de_practicas.Models
{
    public class Inventario
    {
        public int Id { get; set; }

        // Ubicación: Aula o Laboratorio
        public int? AulaId { get; set; }
        public Aulas? Aula { get; set; }

        public int? LaboratorioId { get; set; }
        public Laboratorios? Laboratorio { get; set; }

        // Stock en esa ubicación
        public int Stock { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}