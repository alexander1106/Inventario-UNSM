using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Models
{
    public class Traslado
    {
        public int Id { get; set; }

        // Artículo que se está trasladando
        public int ArticuloId { get; set; }
        public required Articulo Articulo { get; set; }

        // Ubicación de origen
        public int UbicacionOrigenId { get; set; }
        public required Ubicacion UbicacionOrigen { get; set; }

        // Ubicación destino
        public int UbicacionDestinoId { get; set; }
        public required Ubicacion UbicacionDestino { get; set; }

        public int Cantidad { get; set; }  // Cantidad trasladada
        public DateTime FechaTraslado { get; set; } = DateTime.Now;

        // Usuario que realizó el traslado
        public int UsuarioId { get; set; }
        public required Usuario Usuario { get; set; }

        public string Estado { get; set; } = "Completado";

        // Constructor vacío para EF Core
        public Traslado() { }

        // Opcional: constructor solo con propiedades simples
        public Traslado(int cantidad, string estado = "Completado")
        {
            Cantidad = cantidad;
            Estado = estado;
        }
    }
}
