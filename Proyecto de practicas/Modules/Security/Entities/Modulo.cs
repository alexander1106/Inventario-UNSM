namespace Proyecto_de_practicas.Modules.Security.Entities
{
    public class Modulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Ruta { get; set; }
        public string? Icon { get; set; }
        public int Estado { get; set; } = 1;

        public ICollection<SubModulo> SubModulos { get; set; } = new List<SubModulo>();
    }
}