namespace Proyecto_de_practicas.Models
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // Relación con Equipos
        public ICollection<Equipos> Equipos { get; set; }

        // Constructor vacío obligatorio para EF Core
        public Categorias()
        {
            Equipos = new List<Equipos>();
        }

        // Constructor opcional para tu uso en código
        public Categorias(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Equipos = new List<Equipos>();
        }
    }
}