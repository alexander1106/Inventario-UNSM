using Proyecto_de_practicas.Modules.Articulos.Entities;

namespace Proyecto_de_practicas.Modules.Mantenimiento.DTO
{
    public class MantenimientosCreateDTO
    {
        public int ArticuloId { get; set; }
        public DateTime FechaMantenimiento { get; set; }
        public String ProveedorServicion { get; set; }
        public Double Costo { get; set; }
        public String TipoMantenimiento { get; set; }
        public String? Observaciones { get; set; }

    }
}