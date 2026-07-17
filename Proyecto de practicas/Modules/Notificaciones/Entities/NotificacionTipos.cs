namespace Proyecto_de_practicas.Modules.Notificaciones.Entities
{
    public static class NotificacionTipos
    {
        public const string VidaUtilProxima = "VidaUtilProxima";
        public const string PrestamoPendiente = "PrestamoPendiente";
        public const string PrestamoAprobado = "PrestamoAprobado";
        public const string MantenimientoRegistrado = "MantenimientoRegistrado";
        public const string MantenimientoCompletado = "MantenimientoCompletado";
        public const string TrasladoRegistrado = "TrasladoRegistrado";
        public const string TrasladoFirmado = "TrasladoFirmado";

        public static readonly string[] RolesAprobadores = { "Administrador", "superadmin" };
    }
}
