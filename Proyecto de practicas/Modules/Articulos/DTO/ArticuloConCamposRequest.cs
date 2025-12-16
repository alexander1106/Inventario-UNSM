namespace Proyecto_de_practicas.Modules.Articulos.DTO
{
    public class ArticuloConCamposRequest
    {
        public string CodigoPatrimonial { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaAdquision { get; set; }
        public decimal ValorAdquisitivo { get; set; }
        public string Condicion { get; set; } = string.Empty;
        public int TipoArticuloId { get; set; }
        public int UbicacionId { get; set; }
        public int Estado { get; set; }
        public int VidaUtil { get; set; }

        public List<CampoValorDto> CamposValores { get; set; } = new();
    }
}
