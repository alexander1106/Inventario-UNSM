namespace Proyecto_de_practicas.Modules.Security.Entities
{
    public class Permiso
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public ICollection<RolSubModuloPermiso> RolSubModuloPermisos { get; set; }
    }
}