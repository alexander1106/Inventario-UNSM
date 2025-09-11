namespace Proyecto_de_practicas.Models
{
    public class Equipos
    {
        public int Id { get; set; }
        public int CodigoInventario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Marca {  get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Responsable { get; set; } = string.Empty;
        public int CategoriaId { get; set; }


        public Categorias? Categoria { get; set; }
    }
}
