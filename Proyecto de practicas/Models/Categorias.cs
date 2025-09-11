namespace Proyecto_de_practicas.Models
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Equipos> Equipos { get; set; } = new List<Equipos>();
    }
}
