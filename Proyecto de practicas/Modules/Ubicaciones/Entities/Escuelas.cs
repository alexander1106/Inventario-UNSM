using System.Text.Json.Serialization;
using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Ubicaciones.Entities
{
    public class Escuelas
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? ImagenUrl { get; set; }

        public int FacultadId { get; set; }
        public int? UsuarioId { get; set; }
        public int? TecnicoId { get; set; }

        [JsonIgnore]
        public Facultades? Facultad { get; set; }

        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }

        [JsonIgnore]
        public virtual Usuario? Tecnico { get; set; }

        // Relación 1:N
        public virtual ICollection<Ubicacion> Ubicaciones { get; set; }
            = new List<Ubicacion>();
    }
}
