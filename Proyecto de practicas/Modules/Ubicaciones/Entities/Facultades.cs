using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Entities
{
    public class Facultades
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; }
        public Boolean Estado { get; set; }
        public int SedeId { get; set; }

        [JsonIgnore]
        public Sedes? Sede { get; set; }

        [JsonIgnore]
        public ICollection<Escuelas> Escuelas { get; set; } = new List<Escuelas>();
    }
}