using System.Collections.Generic;

namespace Proyecto_de_practicas.Modules.Ubicaciones.DTO
{
    public class UbicacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Piso { get; set; }

        public int TipoUbicacionId { get; set; }


        public int? UsuarioId { get; set; }
        public int? EscuelaId { get; set; }




    }
}
