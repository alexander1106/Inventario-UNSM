namespace Proyecto_de_practicas.Modules.Security.DTO
{
    public class RolSubModuloDTO
    {
        public int RolId { get; set; }
        public int SubModuloId { get; set; }
        public bool PuedeVer { get; set; }
        public bool PuedeCrear { get; set; }
        public bool PuedeEditar { get; set; }
        public bool PuedeEliminar { get; set; }
    }
}
