using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Articulos.Entities
{
    public class Articulo
    {
        public int Id { get; set; }
        public string? QRCodeBase64 { get; set; }
        public string? CodigoPatrimonial { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaAdquision { get; set; }
        public double ValorAdquisitivo { get; set; }
        public string? Condicion { get; set; }

        [Column("vidaUtil")]
        public int VidaUtil { get; set; }
        public int TipoArticuloId { get; set; }
        public virtual TipoArticulo TipoArticulo { get; set; } = null!;
        public int? UbicacionId { get; set; }

        [JsonIgnore]
        public virtual Ubicacion? Ubicacion { get; set; }

        public int Estado { get; set; } = 1;

        // 👇 relación 1 a muchos
        public virtual ICollection<Prestamos> Prestamos { get; set; } = new List<Prestamos>();
    }
}
