namespace Proyecto_de_practicas.DTO
{
    public class EquiposDto
    {
        public int Id { get; set; }
        public int CodigoInventario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Responsable { get; set; } = string.Empty;

    }
}
