namespace Proyecto_de_practicas.Models
{
    public class ArticuloCampoValor
    {
        public int Id { get; set; }

        // 🔗 Relación con Articulo
        public int ArticuloId { get; set; }
        public virtual Articulo Articulo { get; set; } = null!;

        // 🔗 Relación con CampoArticulo
        public int CampoArticuloId { get; set; }
        public virtual CampoArticulo CampoArticulo { get; set; } = null!;

        // 🔢 Se guarda como texto y se interpreta según TipoDato
        public string Valor { get; set; } = null!;
    }
}