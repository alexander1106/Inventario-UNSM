namespace Proyecto_de_practicas.Modules.Security.Entities
{
    public class Roles
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Estado { get; set; } = 1;
    }
}