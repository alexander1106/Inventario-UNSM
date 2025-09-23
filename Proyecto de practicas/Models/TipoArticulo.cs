using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Models
{
    public class TipoArticulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!; 
        public string Descripcion { get; set; } = null!;
        public int Estado { get; set; } = 1;
        // Ruta del archivo en el servidor
        [NotMapped]
        public IFormFile Imagen { get; set; }   // archivo subido
       
        // 📝 Ruta de la imagen que se guarda en la BD
        public string? ImagenPath { get; set; }
        // Relación: Un tipo de artículo puede tener muchos campos
        public virtual ICollection<CampoArticulo> Campos { get; set; } = new List<CampoArticulo>();

        // Relación: Un tipo de artículo puede tener muchos artículos
        public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();


    }
}
