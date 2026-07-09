using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Notificaciones.DTO;
using Proyecto_de_practicas.Modules.Notificaciones.Services;

namespace Proyecto_de_practicas.Modules.Notificaciones.Controller
{
    [Route("api/notificaciones")]
    [ApiController]
    [Authorize]
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionService _service;

        public NotificacionController(INotificacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<NotificacionDto>>>> GetAll([FromQuery] bool? soloNoLeidas)
        {
            var result = await _service.GetAllAsync(User.Identity!.Name!, soloNoLeidas);
            return Ok(new ApiResponse<List<NotificacionDto>>(
                true,
                "Lista de notificaciones obtenida correctamente",
                result
            ));
        }

        [HttpGet("no-leidas/count")]
        public async Task<ActionResult<ApiResponse<int>>> CountNoLeidas()
        {
            var result = await _service.CountNoLeidasAsync(User.Identity!.Name!);
            return Ok(new ApiResponse<int>(
                true,
                "Cantidad de notificaciones no leídas obtenida correctamente",
                result
            ));
        }

        [HttpPut("{id}/marcar-leida")]
        public async Task<ActionResult<ApiResponse<NotificacionDto>>> MarcarLeida(int id)
        {
            var result = await _service.MarcarLeidaAsync(id, User.Identity!.Name!);
            if (result == null)
            {
                return NotFound(new ApiResponse<NotificacionDto>(
                    false,
                    "No se encontró la notificación",
                    null
                ));
            }

            return Ok(new ApiResponse<NotificacionDto>(
                true,
                "Notificación marcada como leída",
                result
            ));
        }

        [HttpPut("marcar-todas-leidas")]
        public async Task<ActionResult<ApiResponse<object>>> MarcarTodasLeidas()
        {
            await _service.MarcarTodasLeidasAsync(User.Identity!.Name!);
            return Ok(new ApiResponse<object>(
                true,
                "Todas las notificaciones fueron marcadas como leídas",
                null
            ));
        }
    }
}
