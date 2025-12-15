using Proyecto_de_practicas.Modules.Articulos.DTO;

public class ArticuloDto
{
    public int Id { get; set; }
    public string QRCodeBase64 { get; set; }

    public string? CodigoPatrimonial { get; set; }
    public string? Nombre { get; set; }
    public DateTime FechaAdquision { get; set; }
    public double ValorAdquisitivo { get; set; }
    public String? Condicion { get; set; }

    public int TipoArticuloId { get; set; }
    public int? UbicacionId { get; set; }
    public int? VidaUtil { get; set; }

    public int Estado { get; set; } = 1;

    public List<ArticuloCampoValorDto> CamposValores { get; set; } = new();

}
