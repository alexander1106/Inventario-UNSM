using Proyecto_de_practicas.Modules.Articulos.Entities;

namespace Proyecto_de_practicas.Modules.Prestamos.DTO
{
    public class PrestamoDTO
    {
        public int Id { get; set; }

        public string? NombreSolicitante { get; set; }
        public DateTime? FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public int Estado { get; set; } = 1;
        public int SolicitanteId { get; set; }
        public string RutaPdf { get; set; }
        public bool Aprobar { get; set; }
        public int ArticuloId { get; set; }
        public bool EstadoPrestamo { get; set; }
        public string? FirmadoPor { get; set; }
        public DateTime? FechaFirma { get; set; }
    }
}