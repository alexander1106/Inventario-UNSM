using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Notificaciones.Services;
using Proyecto_de_practicas.Modules.Traslados.Entities;
using Proyecto_de_practicas.Modules.Traslados.Repository.IRespository;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Drawing;

namespace Proyecto_de_practicas.Modules.Traslados.Repository
{
    public class TrasladoRepository : ITrasladoRepository
    {
        private readonly AplicationDBContext _context;
        private readonly DbSet<Traslado> _dbSet;
        private readonly INotificacionService _notificacionService;

        public TrasladoRepository(AplicationDBContext context, INotificacionService notificacionService)
        {
            _context = context;
            _dbSet = context.Set<Traslado>();
            _notificacionService = notificacionService;
        }

        // 🔹 MÉTODO CLAVE IMPLEMENTADO
        public async Task<bool> RealizarTrasladoAsync(Traslado traslado)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Obtener artículo
                var articulo = await _context.Set<Articulo>()
                    .FirstOrDefaultAsync(a => a.Id == traslado.ArticuloId);

                if (articulo == null)
                    return false;

                // 2️⃣ Validar ubicación origen
                if (articulo.UbicacionId != traslado.UbicacionOrigenId)
                    throw new Exception("El artículo no se encuentra en la ubicación origen");

                // 3️⃣ Mover artículo
                articulo.UbicacionId = traslado.UbicacionDestinoId;

                // 4️⃣ Guardar traslado
                await _dbSet.AddAsync(traslado);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var nombreUsuario = await ObtenerNombreUsuarioAsync(traslado.UsuarioId);
                await _notificacionService.NotificarTrasladoRegistradoAsync(traslado.Id, traslado.ArticuloId, nombreUsuario);

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<string?> ObtenerNombreUsuarioAsync(int usuarioId)
        {
            return await _context.Usuarios
                .Where(u => u.Id == usuarioId)
                .Select(u => u.Nombre)
                .FirstOrDefaultAsync();
        }

        // ---------------- CRUD NORMAL ----------------

        public async Task<List<Traslado>> GetAllAsync()
        {
            return await _dbSet
                .Include(t => t.Articulo)
                .Include(t => t.UbicacionOrigen)
                .Include(t => t.UbicacionDestino)
                .Include(t => t.Usuario)
                .ToListAsync();
        }

        public async Task<Traslado?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Articulo)
                .Include(t => t.UbicacionOrigen)
                .Include(t => t.UbicacionDestino)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Traslado> CreateAsync(Traslado traslado)
        {
            await _dbSet.AddAsync(traslado);
            await _context.SaveChangesAsync();

            var nombreUsuario = await ObtenerNombreUsuarioAsync(traslado.UsuarioId);
            await _notificacionService.NotificarTrasladoRegistradoAsync(traslado.Id, traslado.ArticuloId, nombreUsuario);

            return traslado;
        }

        public async Task<Traslado> UpdateAsync(Traslado traslado)
        {
            _dbSet.Update(traslado);
            await _context.SaveChangesAsync();
            return traslado;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var traslado = await _dbSet.FindAsync(id);
            if (traslado == null) return false;

            _dbSet.Remove(traslado);
            await _context.SaveChangesAsync();
            return true;
        }

        // ---------------- DOCUMENTO / FIRMA ----------------

        public async Task<string> UploadPdfAsync(int trasladoId, IFormFile file)
        {
            var traslado = await _dbSet.FindAsync(trasladoId);

            if (traslado == null)
                throw new Exception("Traslado no encontrado");

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Traslados");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = $"traslado_{trasladoId}_{Guid.NewGuid()}.pdf";
            var fullPath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            traslado.RutaPdf = $"Uploads/Traslados/{fileName}";

            await _context.SaveChangesAsync();

            return traslado.RutaPdf;
        }

        private void AgregarMarcaDeAgua(string rutaPdf, string firmante, DateTime fechaFirma)
        {
            var document = PdfReader.Open(rutaPdf, PdfDocumentOpenMode.Modify);

            var fontAprobado = new XFont("Arial", 72, XFontStyle.Bold);
            var fontInfo    = new XFont("Arial", 11, XFontStyle.Regular);
            var brushRojo   = new XSolidBrush(XColor.FromArgb(55, 200, 0, 0));
            var brushGris   = new XSolidBrush(XColor.FromArgb(110, 60, 60, 60));

            foreach (var page in document.Pages)
            {
                var gfx = XGraphics.FromPdfPage(page);
                double cx = page.Width  / 2;
                double cy = page.Height / 2;

                // "APROBADO" diagonal
                var estado = gfx.Save();
                gfx.TranslateTransform(cx, cy);
                gfx.RotateTransform(-45);
                gfx.DrawString("APROBADO", fontAprobado, brushRojo,
                    new XRect(-250, -45, 500, 90), XStringFormats.Center);
                gfx.Restore(estado);

                // Info centrada debajo del texto diagonal
                gfx.DrawString(
                    $"Firmado por: {firmante}   |   Fecha: {fechaFirma:dd/MM/yyyy HH:mm}",
                    fontInfo, brushGris,
                    new XRect(0, cy + 55, page.Width, 20),
                    XStringFormats.TopCenter);
            }

            document.Save(rutaPdf);
        }

        public async Task<Traslado?> FirmarTrasladoAsync(int id, string firmante)
        {
            var traslado = await _dbSet.FindAsync(id);
            if (traslado == null)
                return null;

            if (string.IsNullOrWhiteSpace(traslado.RutaPdf))
                throw new Exception("El traslado no tiene un PDF adjunto para firmar.");

            if (traslado.FirmadoPor != null)
                throw new Exception("Este traslado ya fue firmado.");

            var rutaAbsoluta = Path.Combine(Directory.GetCurrentDirectory(), traslado.RutaPdf);
            if (!File.Exists(rutaAbsoluta))
                throw new Exception("No se encontró el archivo PDF del traslado.");

            var fechaFirma = DateTime.Now;
            AgregarMarcaDeAgua(rutaAbsoluta, firmante, fechaFirma);

            traslado.FirmadoPor = firmante;
            traslado.FechaFirma = fechaFirma;
            await _context.SaveChangesAsync();

            await _notificacionService.ResolverNotificacionesTrasladoAsync(traslado.Id);
            await _notificacionService.NotificarTrasladoFirmadoAsync(traslado.Id, traslado.ArticuloId, traslado.UsuarioId);

            return traslado;
        }
    }
}
