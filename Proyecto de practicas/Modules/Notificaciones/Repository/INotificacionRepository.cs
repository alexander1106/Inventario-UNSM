using Proyecto_de_practicas.Modules.Notificaciones.Entities;

namespace Proyecto_de_practicas.Modules.Notificaciones.Repository
{
    public interface INotificacionRepository
    {
        Task<List<Notificacion>> GetAllAsync(int usuarioDestinoId, bool? soloNoLeidas);
        Task<int> CountNoLeidasAsync(int usuarioDestinoId);
        Task<Notificacion?> GetByIdAsync(int id);
        Task<Notificacion> AddAsync(Notificacion entity);
        Task<bool> ExisteNotificacionActivaAsync(string tipo, int? articuloId, int? prestamoId, int usuarioDestinoId);
        Task<Notificacion?> MarcarLeidaAsync(int id, int usuarioDestinoId);
        Task MarcarTodasLeidasAsync(int usuarioDestinoId);
        Task MarcarLeidasPorPrestamoAsync(int prestamoId);
    }
}
