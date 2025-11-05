namespace Proyecto_de_practicas.DTO
{
    public class ArticuloCampoValorDto
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public int CampoArticuloId { get; set; }
        public string Valor { get; set; } = null!;
    }
}