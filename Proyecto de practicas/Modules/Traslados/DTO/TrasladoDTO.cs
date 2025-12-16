namespace Proyecto_de_practicas.Modules.Traslados.DTO
{
    public class TrasladoDTO
    {
        public int ArticuloId { get; set; }
        public int UbicacionOrigenId { get; set; }
        public int UbicacionDestinoId { get; set; }
        public DateTime FechaTraslado { get; set; }
        public int UsuarioId { get; set; }
        public string Observaciones { get; set; }
    }
}
