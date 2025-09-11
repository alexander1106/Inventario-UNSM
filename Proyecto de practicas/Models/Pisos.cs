namespace Proyecto_de_practicas.Models
{
    public class Pisos
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int FacultadId { get; set; }

        
        public Facultades? Facultad { get; set; }
    }
}
