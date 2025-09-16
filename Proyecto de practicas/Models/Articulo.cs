namespace Proyecto_de_practicas.Models
{
    public abstract class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Estado { get; set; } = "Activo"; // Borrado lógico
        protected Articulo(string nombre, string estado = "Activo")
        {
            Nombre = nombre;
            Estado = estado;
        }

    }

}
