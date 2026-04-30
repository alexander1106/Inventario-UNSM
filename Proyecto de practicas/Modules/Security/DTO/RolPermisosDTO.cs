using Proyecto_de_practicas.Modules.Security.Entities;

namespace Proyecto_de_practicas.Modules.Security.DTO
{
    public class RolPermisosDTO
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int? ModuloId { get; set; }
        public int? SubModuloId { get; set; }
        public int PermisoId { get; set; }

    }
}