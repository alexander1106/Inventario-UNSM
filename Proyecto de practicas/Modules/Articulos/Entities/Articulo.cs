using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Articulos.Entities
{
    [Table("articulos")]
    public class Articulo
    {
        public int Id { get; set; }
        public string? QRCodeBase64 { get; set; }
        public string? CodigoPatrimonial { get; set; }
        public string? Nombre { get; set; } // 👈 Aquí se guardará lo que venga en "descripcion" del Excel
        public DateTime FechaAdquision { get; set; }
        public double ValorAdquisitivo { get; set; }
        public string? Condicion { get; set; }

        [Column("vidaUtil")]
        public int VidaUtil { get; set; }
        public int TipoArticuloId { get; set; }
        public virtual TipoArticulo TipoArticulo { get; set; } = null!;
        public int? UbicacionId { get; set; }

        // ==============================================================
        // ✨ NUEVOS CAMPOS PARA LA SUBIDA MASIVA DESDE EXCEL
        // ==============================================================
        public string? CodigoBarra { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? NroSerie { get; set; }
        public string? Medidas { get; set; }
        public string? Color { get; set; }
        public string? Mayor { get; set; }
        public string? SubCta { get; set; }

        // Campos de control e historial de depreciación contable
        // Modifica estas propiedades dentro de Articulo.cs
        public decimal HValorInicial { get; set; }
        public decimal HDeprInicial { get; set; }
        public decimal HDeprAjustada { get; set; }
        public decimal HDeprEjercicio { get; set; }
        public decimal ValorNeto { get; set; }
        // ==============================================================

        [JsonIgnore]
        public virtual Ubicacion? Ubicacion { get; set; }

        public int Estado { get; set; } = 1;

        // 👇 relación 1 a muchos
        public virtual ICollection<Prestamos> Prestamos { get; set; } = new List<Prestamos>();
    }
}