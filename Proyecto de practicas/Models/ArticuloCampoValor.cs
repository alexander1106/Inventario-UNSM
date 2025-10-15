namespace Proyecto_de_practicas.Models
{
    public class ArticuloCampoValor
    {
     public int Id { get; set; }
    public int CampoArticuloId { get; set; }
    public CampoArticulo CampoArticulo { get; set; } = null!;

    public string Valor { get; set; } = null!; // Se guarda como string y se interpreta según TipoDato
}
}
