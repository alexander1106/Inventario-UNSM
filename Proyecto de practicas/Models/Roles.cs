namespace Proyecto_de_practicas.Models
{
    public class Roles
    {
        public int Id { get; set; } // EF Core lo hará autoincrementable
        public string Nombre { get; set; }

        // Relación inversa
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}