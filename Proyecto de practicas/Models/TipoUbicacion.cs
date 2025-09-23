namespace Proyecto_de_practicas.Models
{
    public class TipoUbicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!; // "Aula", "Laboratorio", "Oficina", etc.
    }
}
