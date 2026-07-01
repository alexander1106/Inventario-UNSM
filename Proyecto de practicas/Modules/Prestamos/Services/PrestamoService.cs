namespace Proyecto_de_practicas.Modules.Prestamos.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Prestamos.DTO;
using Proyecto_de_practicas.Modules.Prestamos.Services.IServices;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Drawing;
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

    public async Task<IEnumerable<PrestamoDTO>> GetByUbicacionAsync(int tipoUbicacionId)
    {
        var prestamos = await _context.Prestamos
            .Include(p => p.Articulo)
                .ThenInclude(a => a.Ubicacion)
            .Where(p =>
              
                p.Articulo.Ubicacion.TipoUbicacionId == tipoUbicacionId
            )
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
    private void AgregarSello(string rutaPdf)
    {
        var document = PdfReader.Open(rutaPdf, PdfDocumentOpenMode.Modify);

        foreach (var page in document.Pages)
        {
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 40, XFontStyle.Bold);
            gfx.DrawString(
                "APROBADO",
                font,
                XBrushes.Red,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center
            );
        }

        document.Save(rutaPdf);
    }

    private void AgregarFirma(string rutaPdf, string firmante, DateTime fechaFirma)
    {
        var document = PdfReader.Open(rutaPdf, PdfDocumentOpenMode.Modify);
        var lastPage = document.Pages[document.PageCount - 1];
        var gfx = XGraphics.FromPdfPage(lastPage);

        var fontFirma = new XFont("Arial", 12, XFontStyle.Bold);
        var fontFecha = new XFont("Arial", 10, XFontStyle.Regular);
        var pen = new XPen(XColors.Black, 1.5);

        double margen = 60;
        double anchoRect = 220;
        double altoRect = 55;
        double baseY = lastPage.Height - margen - altoRect;

        // Rectángulo contenedor
        gfx.DrawRectangle(pen, margen, baseY, anchoRect, altoRect);

        // Texto "Firmado por: nombre"
        gfx.DrawString(
            $"Firmado por: {firmante}",
            fontFirma,
            XBrushes.Black,
            new XRect(margen + 8, baseY + 10, anchoRect - 16, 20),
            XStringFormats.TopLeft
        );

        // Fecha de firma
        gfx.DrawString(
            $"Fecha: {fechaFirma:dd/MM/yyyy HH:mm}",
            fontFecha,
            XBrushes.Black,
            new XRect(margen + 8, baseY + 30, anchoRect - 16, 20),
            XStringFormats.TopLeft
        );

        document.Save(rutaPdf);
    }

    public async Task<PrestamoDTO?> FirmarPrestamoAsync(int id, string firmante)
    {
        var prestamo = await _context.Prestamos.FindAsync(id);
        if (prestamo == null)
            return null;

        if (string.IsNullOrWhiteSpace(prestamo.RutaPdf))
            throw new Exception("El préstamo no tiene un PDF adjunto para firmar.");

        if (prestamo.FirmadoPor != null)
            throw new Exception("Este préstamo ya fue firmado.");

        var rutaAbsoluta = Path.Combine(Directory.GetCurrentDirectory(), prestamo.RutaPdf);
        if (!File.Exists(rutaAbsoluta))
            throw new Exception("No se encontró el archivo PDF del préstamo.");

        var fechaFirma = DateTime.Now;
        AgregarFirma(rutaAbsoluta, firmante, fechaFirma);

        prestamo.FirmadoPor = firmante;
        prestamo.FechaFirma = fechaFirma;
        prestamo.Aprobar = true;
        await _context.SaveChangesAsync();

        return _mapper.Map<PrestamoDTO>(prestamo);
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