using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.Security
{
    public class RolPermisos
    {
        public int Id { get; set; }

        public int RolId { get; set; }
        public Roles Rol { get; set; }

        // 👇 NUEVO
        public int? ModuloId { get; set; }
        public Modulo? Modulo { get; set; }

        // 👇 AHORA OPCIONAL
        public int? SubModuloId { get; set; }
        public SubModulo? SubModulo { get; set; }
        // 👇 LO QUE TE FALTA
        public int PermisoId { get; set; }
        public Permiso Permiso { get; set; }
    }
}
