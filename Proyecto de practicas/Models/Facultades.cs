namespace Proyecto_de_practicas.Models
{
    public class Facultades
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; } = 1;

        public ICollection<UsuarioFacultadRol> UsuariosFacultadesRoles { get; set; }

    }
}