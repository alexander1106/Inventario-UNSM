using System.Collections.Generic;

namespace Proyecto_de_practicas.Modules.Ubicaciones.DTO
{
    public class UbicacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Piso { get; set; } = 0; 

        // Información del tipo de ubicación
        public int TipoUbicacionId { get; set; }

        // Opcional: lista de artículos en esta ubicación
    }
}
