namespace Proyecto_de_practicas.Modules.Prestamos.DTO
{
    public class SolicitanteDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Ciclo { get; set; } = string.Empty;
        public int UbicacionId { get; set; }
    }
}
