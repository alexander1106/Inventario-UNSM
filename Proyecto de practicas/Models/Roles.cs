namespace Proyecto_de_practicas.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Estado { get; set; } = 1; 
        public ICollection<UsuarioFacultadRol> UsuariosFacultadesRoles { get; set; }

        // Constructor opcional solo con propiedades simples
        public Roles(string nombre, int estado)
        {
            Nombre = nombre;
            Estado = estado; 
        }

        // Constructor vacío para EF Core
        public Roles() { }
    }
}