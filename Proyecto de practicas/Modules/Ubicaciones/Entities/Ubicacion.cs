using System.Text.Json.Serialization;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Entities
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Piso { get; set; } = 0;

        public int TipoUbicacionId { get; set; }

        [JsonIgnore]
        public virtual TipoUbicacion TipoUbicacion { get; set; } = null!;

        public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

        public string? ImagenUrl { get; set; }

        public int? UsuarioId { get; set; }

        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; } = null!;

        // 🔥 NUEVO: jerarquía
        public int? PadreId { get; set; }

        [JsonIgnore]
        public virtual Ubicacion? Padre { get; set; }

        public virtual ICollection<Ubicacion> Hijos { get; set; } = new List<Ubicacion>();
    }
}