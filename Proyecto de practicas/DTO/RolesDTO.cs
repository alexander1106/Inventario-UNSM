namespace Proyecto_de_practicas.DTO
{
    public class RolesDTO
    {
        public string Nombre { get; set; } = null!;
        public int Estado { get; set; } = 1;
        public RolesDTO() { }
        public RolesDTO(string Nombre, int Estado)
        {
            this.Nombre = Nombre;
            this.Estado = Estado;
        }
    }
}
