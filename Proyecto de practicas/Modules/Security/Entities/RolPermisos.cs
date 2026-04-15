using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Security
{
    public class RolPermisos
    {
        public int Id { get; set; }

        public int RolId { get; set; }
        public Roles Rol { get; set; }

        public int SubModuloId { get; set; }
        public SubModulo SubModulo { get; set; }

        // 👇 LO QUE TE FALTA
        public int PermisoId { get; set; }
        public Permiso Permiso { get; set; }
    }
}
