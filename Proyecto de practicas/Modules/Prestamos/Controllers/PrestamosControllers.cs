using Proyecto_de_practicas.Config;
using Proyecto_de_practicas.Modules.Prestamos.DTO;
using Proyecto_de_practicas.Modules.Prestamos.Services.IServices;
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

    // 🔹 GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var prestamos = await _prestamoService.GetAllAsync();

        return Ok(new ApiResponse<object>(
            true,
            "Lista de préstamos obtenida correctamente",
            prestamos
        ));
    }

    // 🔹 GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var prestamo = await _prestamoService.GetByIdAsync(id);

        if (prestamo == null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Préstamo no encontrado",
                null
            ));
        }

        return Ok(new ApiResponse<object>(
            true,
            "Préstamo encontrado",
            prestamo
        ));
    }

    // 🔹 CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePrestamoDTO request)
    {
        try
        {
            var nuevoPrestamo = await _prestamoService.CreateAsync(request);

            return Ok(new ApiResponse<object>(
                true,
                "Préstamo creado correctamente",
                nuevoPrestamo
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "Error al crear préstamo",
                null,
                ex.Message
            ));
        }
    }

    // 🔹 UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePrestamosDTO request)
    {
        var prestamoActualizado = await _prestamoService.UpdateAsync(id, request);

        if (prestamoActualizado == null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Préstamo no encontrado",
                null
            ));
        }

        return Ok(new ApiResponse<object>(
            true,
            "Préstamo actualizado correctamente",
            prestamoActualizado
        ));
    }

    // 🔹 DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _prestamoService.DeleteAsync(id);

        if (!eliminado)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Préstamo no encontrado",
                null
            ));
        }

        return Ok(new ApiResponse<object>(
            true,
            "Préstamo eliminado correctamente",
            null
        ));
    }

    // 🔹 ACTIVOS
    [HttpGet("activos")]
    public async Task<IActionResult> GetActivos()
    {
        var prestamos = await _prestamoService.GetPrestamosActivosAsync();

        return Ok(new ApiResponse<object>(
            true,
            "Préstamos activos obtenidos correctamente",
            prestamos
        ));
    }
}