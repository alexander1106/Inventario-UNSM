using Proyecto_de_practicas.Modules.Articulos.Entities;

namespace Proyecto_de_practicas.Modules.Mantenimiento.Entity
{
    public class Mantenimientos
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public virtual Articulo Articulo { get; set; } = null!;
        public DateTime FechaMantenimiento { get; set; }
        public String ProveedorServicion { get; set; }
        public Double Costo { get; set; }
        public String TipoMantenimiento { get; set; }
        public String? Observaciones { get; set; }
        public Boolean EstadoMantenimiento { get; set; } = true;
        public Mantenimientos() { }
        public Boolean Estado { get; set; } = true;

    }
}