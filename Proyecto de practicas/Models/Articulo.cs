using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Proyecto_de_practicas.Models
{
    public  class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Relación con TipoArticulo
        public int TipoArticuloId { get; set; }
        public virtual TipoArticulo TipoArticulo { get; set; } = null!;

        // Datos dinámicos de los campos en formato JSON
        public int? UbicacionId { get; set; }
        public virtual Ubicacion? Ubicacion { get; set; }

        public int Estado { get; set; } = 1; // Borrado lógico
        public virtual ICollection<ArticuloCampoValor> CamposValores { get; set; } = new List<ArticuloCampoValor>();

    }

}
