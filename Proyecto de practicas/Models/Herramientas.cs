namespace Proyecto_de_practicas.Models
{
    public class Herramientas : Articulo
    {
        public string Color { get; set; }

        // Constructor
        public Herramientas(string nombre, string color, string estado = "Activo") : base(nombre, estado)
        {
            Color = color;
        }
    }
}