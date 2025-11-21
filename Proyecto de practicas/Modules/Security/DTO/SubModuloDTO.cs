namespace Proyecto_de_practicas.Modules.Security.DTO
{
    public class SubModuloDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Ruta { get; set; }

        public string? Icon { get; set; }
        public int ModuloId { get; set; }
    }
}