using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Articulos.Entities
{
    public  class Articulo
    {
        public int Id { get; set; }

        public int TipoArticuloId { get; set; }
        public virtual TipoArticulo TipoArticulo { get; set; } = null!;
        public int Stock { get; set; }

        public int? UbicacionId { get; set; }
        [JsonIgnore] // 👈 evita que te lo pida o muestre
        public virtual Ubicacion? Ubicacion { get; set; }

        public int Estado { get; set; } = 1; // Borrado lógico
        public string QRCodeBase64 { get; set; }

    }

}
