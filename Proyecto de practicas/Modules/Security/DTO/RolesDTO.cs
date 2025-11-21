namespace Proyecto_de_practicas.Modules.Security.DTO
{
    public class RolesDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int Estado { get; set; } = 1; // ✅ propiedad en lugar de campo
    }
}
