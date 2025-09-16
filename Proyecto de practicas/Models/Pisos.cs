using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Models
{
    public class Pisos
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int FacultadId { get; set; }
        
        [JsonIgnore] 
        public Facultades Facultad { get; set; } = null!; // navegación

        public ICollection<Ubicacion> Ubicaciones { get; set; } = new List<Ubicacion>();

        // Constructor
     
    }
}