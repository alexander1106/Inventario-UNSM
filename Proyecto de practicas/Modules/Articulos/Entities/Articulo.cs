using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Articulos.Entities
{
    [Table("Articulos")]
    public class Articulo
    {
        public int Id { get; set; }
        public string? CodigoPatrimonial { get; set; }
        public string? Nombre { get; set; } 
        public DateTime FechaAdquision { get; set; }
        public double ValorAdquisitivo { get; set; }
        public string? Condicion { get; set; }

        
        public int TipoArticuloId { get; set; }
        public virtual TipoArticulo TipoArticulo { get; set; } = null!;
        public int? UbicacionId { get; set; }

        public string? CodigoBarra { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? NroSerie { get; set; }
        public string? OtrasObservaciones { get; set; }
        public double TiempoVidaUtil { get; set; }
        public int? ClasificacionDepreciacionId { get; set; }
        public virtual ClasificacionDepreciacion? ClasificacionDepreciacion { get; set; }
        public double DepreciacionAnual { get; set; }
        public decimal ValorActual { get; set; }
        [JsonIgnore]
        public virtual Ubicacion? Ubicacion { get; set; }
        public int Estado { get; set; } = 1;
        public virtual ICollection<Prestamos> Prestamos { get; set; } = new List<Prestamos>();
    }
}