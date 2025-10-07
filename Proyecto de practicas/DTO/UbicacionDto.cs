using System.Collections.Generic;
using Proyecto_de_practicas.DTO;

namespace Proyecto_de_practicas.DTOs
{
    public class UbicacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        // Información del tipo de ubicación
        public int TipoUbicacionId { get; set; }

        // Opcional: lista de artículos en esta ubicación
    }
}
