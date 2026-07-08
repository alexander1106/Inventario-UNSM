using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Modules.Articulos.Entities
{
    public class ClasificacionDepreciacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public double VidaUtilAnios { get; set; }
        public double PorcentajeDepreciacionAnual { get; set; }
        public int Estado { get; set; } = 1;
        [JsonIgnore]
        public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
    }
}
