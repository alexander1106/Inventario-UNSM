namespace Proyecto_de_practicas.Modules.Reporte.DTO
{
    public class ReporteRequestDto
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? UbicacionId { get; set; }
        public int? UbicacionOrigenId { get; set; }
        public int? UbicacionDestinoId { get; set; }
        public int? CategoriaId { get; set; }
        public string Estado { get; set; } = "Todos"; // 'Nuevo', 'Usado', 'Dañado'
        public int Tipo { get; set; } // 0=Inventario, 1=Prestamos, 2=Mantenimiento, 3=Traslados
    }
}
