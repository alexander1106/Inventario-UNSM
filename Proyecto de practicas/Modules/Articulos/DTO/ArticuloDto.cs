public class ArticuloDto
{
    public int Id { get; set; }
    public string? CodigoPatrimonial { get; set; }
    public string? CodigoBarra { get; set; }
    public string? Nombre { get; set; }
    public DateTime FechaAdquision { get; set; }
    public double ValorAdquisitivo { get; set; }
    public string? Condicion { get; set; }
    public int TipoArticuloId { get; set; }
    public int? UbicacionId { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? NroSerie { get; set; }
    public string? OtrasObservaciones { get; set; }
    public double TiempoVidaUtil { get; set; }
    public int? ClasificacionDepreciacionId { get; set; }
    public double DepreciacionAnual { get; set; }
    public decimal ValorActual { get; set; }
    public int Estado { get; set; }
}