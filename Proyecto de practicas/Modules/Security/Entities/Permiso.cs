using Proyecto_de_practicas.Modules.Security.DTO;
using Proyecto_de_practicas.Modules.Security.Security;

namespace Proyecto_de_practicas.Modules.Security.Entities
{
    public class Permiso
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public ICollection<RolPermisos> RolPermisos { get; set; } = new List<RolPermisos>();

    }

}