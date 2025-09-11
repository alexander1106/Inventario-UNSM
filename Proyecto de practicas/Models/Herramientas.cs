namespace Proyecto_de_practicas.Models
{
    public class Herramientas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public bool Estado { get; set; } = true; // true = Activo, false = Inactivo

        // Relaciones
        public int LaboratorioId { get; set; }
        public Laboratorios Laboratorio { get; set; }

        public int AulaId { get; set; }
        public Aulas Aula { get; set; }
    }
}