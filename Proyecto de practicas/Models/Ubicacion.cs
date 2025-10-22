using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Models
{
    public  class Ubicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        // Relación con TipoUbicacion
        public int TipoUbicacionId { get; set; }
        [JsonIgnore] // 👈 evita el ciclo
        public virtual TipoUbicacion TipoUbicacion { get; set; } = null!;
        public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

    }
}