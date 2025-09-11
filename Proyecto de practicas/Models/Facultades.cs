namespace Proyecto_de_practicas.Models
{
    public class Facultades
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public ICollection<Pisos> Pisos { get; set; } = new List<Pisos>();

    }
}
