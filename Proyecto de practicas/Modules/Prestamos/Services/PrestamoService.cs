namespace Proyecto_de_practicas.Modules.Prestamos.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Prestamos.DTO;
using Proyecto_de_practicas.Modules.Prestamos.Services.IServices;

public class PrestamoService : IServicePrestamos
{
    private readonly AplicationDBContext _context;
    private readonly IMapper _mapper;

    public PrestamoService(AplicationDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PrestamoDTO>> GetAllAsync()
    {
        var prestamos = await _context.Prestamos
            .Include(p => p.Articulo)
            .ToListAsync();

        return prestamos.Select(p => new PrestamoDTO
        {
            Id = p.Id,
            NombreSolicitante = p.NombreSolicitante,
            FechaPrestamo = p.FechaPrestamo,
            FechaDevolucion = p.FechaDevolucion,
            Estado = p.Estado,
            RutaPdf = p.RutaPdf,
            Aprobar= p.Aprobar,
            EstadoPrestamo = p.EstadoPrestamo,
            ArticuloId = p.Articulo.Id,
            SolicitanteId =p.SolicitanteId
        });
    }

    public async Task<IEnumerable<PrestamoDTO>> GetByUbicacionAsync(int ubicacionId)
    {
        var prestamos = await _context.Prestamos
            .Include(p => p.Articulo)
            .Where(p => p.Articulo.UbicacionId == ubicacionId)
            .ToListAsync();

        return prestamos.Select(p => new PrestamoDTO
        {
            Id = p.Id,
            NombreSolicitante = p.NombreSolicitante,
            FechaPrestamo = p.FechaPrestamo,
            FechaDevolucion = p.FechaDevolucion,
            Estado = p.Estado,
            RutaPdf = p.RutaPdf,
            Aprobar = p.Aprobar,
            EstadoPrestamo = p.EstadoPrestamo,
            ArticuloId = p.Articulo.Id,
            SolicitanteId = p.SolicitanteId
        });
    }
    public async Task<string> UploadPdfAsync(int prestamoId, IFormFile file)
    {
        var prestamo = await _context.Prestamos.FindAsync(prestamoId);

        if (prestamo == null)
            throw new Exception("Préstamo no encontrado");

        var folder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Prestamos");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fileName = $"prestamo_{prestamoId}_{Guid.NewGuid()}.pdf";
        var fullPath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        prestamo.RutaPdf = $"Uploads/Prestamos/{fileName}";

        await _context.SaveChangesAsync();

        return prestamo.RutaPdf;
    }
    public async Task<PrestamoDTO?> GetByIdAsync(int id)
    {
        var prestamo = await _context.Prestamos.FindAsync(id);
        return _mapper.Map<PrestamoDTO>(prestamo);
    }

    public async Task<PrestamoDTO> CreateAsync(CreatePrestamoDTO request)
    {
        try
        {
            var articulo = await _context.Articulos.FindAsync(request.ArticuloId);
            if (articulo == null)
                throw new Exception("Artículo no existe");

            var solicitante = await _context.Solicitantes
                .FindAsync(request.SolicitanteId);

            if (solicitante == null)
                throw new Exception("Solicitante no existe");

            var yaPrestado = await _context.Prestamos
                .AnyAsync(p => p.Articulo.Id == request.ArticuloId && p.EstadoPrestamo == true);

            if (yaPrestado)
                throw new Exception("El artículo ya está prestado");

            var prestamo = new Prestamos
            {
                Articulo = articulo,

                Solicitante = solicitante,   // ✅ CORRECTO
                SolicitanteId = solicitante.Id, // ✅ CORRECTO
                Aprobar = false,

                NombreSolicitante = request.NombreSolicitante,
                FechaPrestamo = request.FechaPrestamo,
                FechaDevolucion = request.FechaDevolucion,
                Estado = 1,
                EstadoPrestamo = true
            };

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();

            return _mapper.Map<PrestamoDTO>(prestamo);
        }
        catch (DbUpdateException ex)
        {
            var inner = ex.InnerException?.Message ?? ex.Message;
            throw new Exception($"Error al guardar el préstamo: {inner}");
        }
    }
    // 🔹 Eliminar
    public async Task<bool> DeleteAsync(int id)
    {
        var prestamo = await _context.Prestamos.FindAsync(id);
        if (prestamo == null) return false;

        _context.Prestamos.Remove(prestamo);
        await _context.SaveChangesAsync();

        return true;
    }



    // 🔹 Activos
    public async Task<IEnumerable<PrestamoDTO>> GetPrestamosActivosAsync()
    {
        var prestamos = await _context.Prestamos
            .Where(p => p.Estado == 1)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PrestamoDTO>>(prestamos);
    }


    // 🔹 Existe
    public async Task<bool> ExistePrestamoAsync(int id)
    {
        return await _context.Prestamos.AnyAsync(p => p.Id == id);
    }

    // 🔹 Contar
    public async Task<int> CountAsync()
    {
        return await _context.Prestamos.CountAsync();
    }


    public async Task<PrestamoDTO?> CambiarEstadoAsync(int id, bool estado)
    {
        var prestamo = await _context.Prestamos.FindAsync(id);

        if (prestamo == null)
            return null;

        prestamo.Aprobar = estado; // 👈 AQUÍ cambias a 2

        await _context.SaveChangesAsync();

        return _mapper.Map<PrestamoDTO>(prestamo);
    }
    // 🔹 Actualizar solo el estado del préstamo
    public async Task<PrestamoDTO?> UpdateEstadoPrestamoAsync(int id, bool nuevoEstado)
    {
        var prestamo = await _context.Prestamos.FindAsync(id);
        if (prestamo == null) return null;

        prestamo.EstadoPrestamo = nuevoEstado; // 0 = pendiente, 1 = activo, 2 = devuelto, etc.
        await _context.SaveChangesAsync();

        return _mapper.Map<PrestamoDTO>(prestamo);
    }

    public Task<IEnumerable<PrestamoDTO>> GetByClienteAsync(int clienteId)
    {
        throw new NotImplementedException();
    }

    public Task<PrestamoDTO?> UpdateAsync(int id, UpdatePrestamosDTO request)
    {
        throw new NotImplementedException();
    }
} 