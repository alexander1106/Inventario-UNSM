using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Traslados.Entities
{
    public class Traslado
    {
        public int Id { get; set; }

        public int ArticuloId { get; set; }
        public Articulo? Articulo { get; set; }  // <- poner ? para que sea opcional

        public int UbicacionOrigenId { get; set; }
        public Ubicacion? UbicacionOrigen { get; set; }

        public int UbicacionDestinoId { get; set; }
        public Ubicacion? UbicacionDestino { get; set; }

        public DateTime FechaTraslado { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public string Observaciones { get; set; } = string.Empty;

    
    
}
}
