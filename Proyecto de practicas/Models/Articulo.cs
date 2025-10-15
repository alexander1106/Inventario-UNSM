using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Models
{
    public  class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int TipoArticuloId { get; set; }
        public virtual TipoArticulo TipoArticulo { get; set; } = null!;

        public int? UbicacionId { get; set; }
        [JsonIgnore] // 👈 evita que te lo pida o muestre
        public virtual Ubicacion? Ubicacion { get; set; }

        public int Estado { get; set; } = 1; // Borrado lógico

    }

}
