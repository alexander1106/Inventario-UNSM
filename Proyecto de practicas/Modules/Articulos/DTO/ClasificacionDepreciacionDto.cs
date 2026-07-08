namespace Proyecto_de_practicas.Modules.Articulos.DTO
{
    public class ClasificacionDepreciacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public double VidaUtilAnios { get; set; }
        public double PorcentajeDepreciacionAnual { get; set; }
        public int Estado { get; set; } = 1;
    }
}
