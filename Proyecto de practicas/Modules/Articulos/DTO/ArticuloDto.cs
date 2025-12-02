
namespace Proyecto_de_practicas.Modules.Articulos.DTO
{
    public class ArticuloDto
    {
        public int Id { get; set; }
        public int TipoArticuloId { get; set; }
        public int? UbicacionId { get; set; }
        public int Estado { get; set; } = 1;

    }
}