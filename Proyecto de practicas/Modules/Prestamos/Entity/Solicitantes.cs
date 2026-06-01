using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

      public class Solicitantes
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correro { get; set; }
    public string Telefono { get; set; }
    public string Cargo { get; set; }
    public string Ciclo { get; set; }
    public virtual ICollection<Prestamos> Prestamo { get; set; }
        = new List<Prestamos>();    // FK
    public int UbicacionId { get; set; }

    // Navegación
    public virtual Ubicacion Ubicacion { get; set; } = null!;


}
