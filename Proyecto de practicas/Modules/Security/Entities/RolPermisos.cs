using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Security
{
    public class RolPermisos
    {
        public int Id { get; set; }

        public int RolId { get; set; }
        public virtual Roles Rol { get; set; } = null!;

        public int SubModuloId { get; set; }
        public virtual SubModulo SubModulo { get; set; } = null!;

        // Asegúrate de que PermisoId sea INT y no tenga espacios raros
        public int PermisoId { get; set; }
        public virtual Permiso Permiso { get; set; } = null!;
    }
}
