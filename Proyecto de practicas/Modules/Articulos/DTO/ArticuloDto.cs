using System;
using System.Collections.Generic;
using Proyecto_de_practicas.Modules.Articulos.DTO;

public class ArticuloDto
{
    public int Id { get; set; }
    public string? QRCodeBase64 { get; set; }

    public string? CodigoPatrimonial { get; set; }
    public string? Nombre { get; set; }
    public DateTime FechaAdquision { get; set; }

    // Usamos decimal para precisión financiera en lugar de double
    public decimal ValorAdquisitivo { get; set; }
    public string? Condicion { get; set; }

    public int TipoArticuloId { get; set; }
    public int? UbicacionId { get; set; }
    public int? VidaUtil { get; set; }

    public int Estado { get; set; } = 1;

    // ✨ NUEVOS CAMPOS TÉCNICOS Y LOGÍSTICOS
    public string? CodigoBarra { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? NroSerie { get; set; }
    public string? Medidas { get; set; }
    public string? Color { get; set; }

    // ✨ NUEVOS CAMPOS CONTABLES Y DEPRECIACIÓN
    public string? Mayor { get; set; }
    public string? SubCta { get; set; }
    public decimal HValorInicial { get; set; }
    public decimal HDeprInicial { get; set; }
    public decimal HDeprAjustada { get; set; }
    public decimal HDeprEjercicio { get; set; }
    public decimal ValorNeto { get; set; }

    public List<ArticuloCampoValorDto> CamposValores { get; set; } = new();
}