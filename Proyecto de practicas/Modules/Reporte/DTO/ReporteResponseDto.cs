namespace Proyecto_de_practicas.Modules.Reporte.DTO
{
    public class ReporteResponseDto
    {
        public List<KpiDto> Kpis { get; set; } = new();
        public GraficoDto Grafico { get; set; } = new();
        public List<dynamic> Tabla { get; set; } = new(); // Dinámico porque las columnas cambian
    }
}
