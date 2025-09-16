using System.Text.Json.Serialization;

namespace Proyecto_de_practicas.Models
{
    public abstract class Ubicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Estado { get; set; } = "Activo";

        // FK a Pisos
        public int PisosId { get; set; }
        [JsonIgnore]

        public Pisos Pisos { get; set; } = null!; // navegación

        // Constructor requerido por EF Core
        protected Ubicacion() { }

        // Constructor de conveniencia (solo datos simples)
        public Ubicacion(string nombre, int pisosId, string estado = "Activo")
        {
            Nombre = nombre;
            PisosId = pisosId;
            Estado = estado;
        }
    }
}