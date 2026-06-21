using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Entities
{
    public class Escuelas
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public int FacultadId { get; set; }

        [JsonIgnore]
        public Facultades? Facultad { get; set; }

        // Relación 1:N
        public virtual ICollection<Ubicacion> Ubicaciones { get; set; }
            = new List<Ubicacion>();
    }
}
