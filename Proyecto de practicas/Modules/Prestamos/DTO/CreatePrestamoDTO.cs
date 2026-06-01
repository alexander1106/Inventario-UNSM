namespace Proyecto_de_practicas.Modules.Prestamos.DTO
{
    public class CreatePrestamoDTO
    {
        public int ArticuloId { get; set; }
        public int SolicitanteId { get; set; }
        public string NombreSolicitante { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public int Estado { get; set; } = 1;
        public bool EstadoPrestamo
        {
            get; set;
        }
    }
}
