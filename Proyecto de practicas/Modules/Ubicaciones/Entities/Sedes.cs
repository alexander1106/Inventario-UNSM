using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Entities
{
    public class Sedes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public Boolean Estado { get; set; }

        // Relación 1:N
        [JsonIgnore]
        public ICollection<Facultades> Facultades { get; set; } = new List<Facultades>();
    }
}