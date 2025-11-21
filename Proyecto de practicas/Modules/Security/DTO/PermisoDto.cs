namespace Proyecto_de_practicas.Modules.Security.DTO
{
    public class PermisoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}
