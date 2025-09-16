namespace SistemaInventario.DTO
{
    public class AulasDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Estado { get; set; } = "Activo";

        // FK a Pisos
        public int PisosId { get; set; }
    }
}
