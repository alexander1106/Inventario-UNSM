using Proyecto_de_practicas.Modules.Security.Security;

namespace Proyecto_de_practicas.Modules.Security.Entities
{
    public class RolSubModuloPermiso
    {
        public int Id { get; set; }

        public int RolSubModuloId { get; set; }
        public RolSubModulo RolSubModulo { get; set; }

        public int PermisoId { get; set; }
        public Permiso Permiso { get; set; }
    }
}
