using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Models
{
    public class Inventario
    {
        public int Id { get; set; }
        // Relación con artículo (Herramienta o Equipo, porque heredan de Articulo)
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }

        // Relación con ubicación
        public int UbicacionId { get; set; }
        public Ubicacion Ubicacion { get; set; }




        // Información adicional del inventario
        public int Cantidad { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public string Estado { get; set; } = "Disponible";
        // Constructor
        // ⚠ Constructor eliminado o vacío
        public Inventario() { }

        // Opcional: constructor solo con propiedades simples
        public Inventario(int cantidad, string estado = "Disponible")
        {
            Cantidad = cantidad;
            Estado = estado;
        }
    }
}