using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Modules.Articulos.Entities
{
    public class TipoArticulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!; 
        public string Descripcion { get; set; } = null!;
        public int Estado { get; set; } = 1;
        [NotMapped]
        public IFormFile Imagen { get; set; }   // archivo subido
        public string? ImagenPath { get; set; }
        public virtual ICollection<CampoArticulo> Campos { get; set; } = new List<CampoArticulo>();
        public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
    }
}
