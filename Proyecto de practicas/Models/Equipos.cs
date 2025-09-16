namespace Proyecto_de_practicas.Models
{
    public class Equipos : Articulo
    {
        public int CodigoInventario { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;

        public int CategoriaId { get; set; }
        public Categorias? Categoria { get; set; }

        // Constructor vacío obligatorio para EF Core
        public Equipos() : base(string.Empty, "Activo")
        {
        }

        // Constructor personalizado para uso en tu código
        public Equipos(
            string nombre,
            string marca,
            string modelo,
            int codigoInventario = 0,
            string descripcion = "",
            Categorias? categoria = null,
            string estado = "Activo"
        ) : base(nombre, estado)
        {
            Marca = marca;
            Modelo = modelo;
            CodigoInventario = codigoInventario;
            Descripcion = descripcion;
            Categoria = categoria;
            CategoriaId = categoria?.Id ?? 0;
        }
    }
}
