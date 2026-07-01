using System.Collections.Generic;

namespace Proyecto_de_practicas.Modules.Articulos.DTO
{
    public class ArticuloConCamposRequest
    {
        public string? CodigoPatrimonial { get; set; }
        public string? Nombre { get; set; }
        public double ValorAdquisitivo { get; set; }
        public string? Condicion { get; set; }
        public int TipoArticuloId { get; set; }
        public int? UbicacionId { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? NroSerie { get; set; }
        public string? Medidas { get; set; }
        public string? Color { get; set; }
        public double TiempoVidaUtil { get; set; }
        public List<CampoValorDto>? CamposValores { get; set; } = new();
    }
}