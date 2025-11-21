using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Security
{
    public class RolSubModulo
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public Roles Rol { get; set; }

        public int SubModuloId { get; set; }
        public SubModulo SubModulo { get; set; }
        public ICollection<RolSubModuloPermiso> Permisos { get; set; }
    }
}
