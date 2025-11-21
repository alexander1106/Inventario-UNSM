namespace Proyecto_de_practicas.Modules.Security.Entities
{
    public class SubModulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Ruta { get; set; }
        public string? Icon { get; set; }
        public int ModuloId { get; set; }
        public Modulo Modulo { get; set; }
        public int Estado { get; set; } = 1;
    }
}
