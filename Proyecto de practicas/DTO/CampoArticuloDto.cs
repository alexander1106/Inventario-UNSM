namespace Proyecto_de_practicas.DTO
{
    public class CampoArticuloDto
    {
        public int Id { get; set; }
        public string NombreCampo { get; set; } = null!;
        public string TipoDato { get; set; } = "string";
        public int TipoArticuloId { get; set; }
    }
}