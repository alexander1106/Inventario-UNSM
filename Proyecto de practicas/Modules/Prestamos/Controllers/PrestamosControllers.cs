using global::Proyecto_de_practicas.Modules.Prestamos.DTO;
using global::Proyecto_de_practicas.Modules.Prestamos.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_de_practicas.Modules.Prestamos.Controllers;

[ApiController]
[Route("api/prestamos")]
public class PrestamosController : ControllerBase
{
    private readonly IServicePrestamos _prestamoService;

    public PrestamosController(IServicePrestamos prestamoService)
    {
        _prestamoService = prestamoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var prestamos = await _prestamoService.GetAllAsync();
        return Ok(prestamos);
    }

    // 🔹 Obtener por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var prestamo = await _prestamoService.GetByIdAsync(id);

        if (prestamo == null)
            return NotFound(new { mensaje = "Préstamo no encontrado" });

        return Ok(prestamo);
    }

    // 🔹 Crear préstamo
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePrestamoDTO request)
    {
        try
        {
            var nuevoPrestamo = await _prestamoService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = nuevoPrestamo.Id },
                nuevoPrestamo
            );
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    // 🔹 Actualizar préstamo
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, int nuevoEstado)
    {
        var prestamoActualizado = await _prestamoService.UpdateEstadoAsync(id, nuevoEstado);

        if (prestamoActualizado == null)
            return NotFound(new { mensaje = "Préstamo no encontrado" });

        return Ok(prestamoActualizado);
    }

    // 🔹 Eliminar préstamo
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _prestamoService.DeleteAsync(id);

        if (!eliminado)
            return NotFound(new { mensaje = "Préstamo no encontrado" });

        return Ok(new { mensaje = "Préstamo eliminado correctamente" });
    }

    // 🔹 Obtener préstamos activos
    [HttpGet("activos")]
    public async Task<IActionResult> GetActivos()
    {   
        var prestamos = await _prestamoService.GetPrestamosActivosAsync();
        return Ok(prestamos);
    }


}
