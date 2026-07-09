using Proyecto_de_practicas.Modules.Notificaciones.DTO;

namespace Proyecto_de_practicas.Modules.Notificaciones.Services
{
    public interface INotificacionService
    {
        Task<List<NotificacionDto>> GetAllAsync(string username, bool? soloNoLeidas);
        Task<int> CountNoLeidasAsync(string username);
        Task<NotificacionDto?> MarcarLeidaAsync(int id, string username);
        Task MarcarTodasLeidasAsync(string username);
        Task NotificarPrestamoPendienteAsync(int prestamoId, int articuloId, string? nombreSolicitante);
        Task NotificarVidaUtilProximaAsync(int articuloId, string mensaje);
        Task NotificarPrestamoAprobadoAsync(int prestamoId, int articuloId, int usuarioRegistroId);
        Task ResolverNotificacionesPrestamoAsync(int prestamoId);
        Task NotificarMantenimientoRegistradoAsync(int articuloId, string tipoMantenimiento);
        Task NotificarMantenimientoCompletadoAsync(int articuloId);
    }
}
