namespace Proyecto_de_practicas.Modules.Ubicaciones.DTO
{
    public class FacultadesDetalleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int NroEscuelas { get; set; }
        public int NroBienes { get; set; }
        public Boolean Estado { get; set; }
        // ✅ AGREGAR ESTO
        public int SedeId { get; set; }
    }
}

