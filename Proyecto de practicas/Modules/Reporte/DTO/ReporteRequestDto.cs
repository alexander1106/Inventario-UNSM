namespace Proyecto_de_practicas.Modules.Reporte.DTO
{

    public class ReporteRequestDto
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public List<int> UbicacionIds { get; set; } = new(); // 🔥 NUEVO

        public int? UbicacionOrigenId { get; set; }
        public int? UbicacionDestinoId { get; set; }

        public int? CategoriaId { get; set; }
        public string Estado { get; set; } = "Todos";
        public int Tipo { get; set; }
    }
}
