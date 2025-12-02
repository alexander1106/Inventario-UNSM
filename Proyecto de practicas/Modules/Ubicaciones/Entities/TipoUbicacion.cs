using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Entities
{
    public class TipoUbicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!; // "Aula", "Laboratorio", "Oficina", etc.
        [JsonIgnore] // 👈 evita el ciclo
        public virtual ICollection<Ubicacion> Ubicaciones { get; set; } = new List<Ubicacion>();
    }
}
