namespace Proyecto_de_practicas.DTO
{
    public class TipoUbicacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!; // "Aula", "Laboratorio", "Oficina", etc.
    }
}
