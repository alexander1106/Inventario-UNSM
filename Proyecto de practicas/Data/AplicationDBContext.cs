using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;



namespace Proyecto_de_practicas.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) { }
  
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Pisos> Pisos { get; set; }
        public DbSet<Facultades> Facultades { get; set; }

        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Traslado> Traslado { get; set; }

        public DbSet<TipoArticulo> TipoArticulos{ get; set; }
        public DbSet<UsuarioFacultadRol> UsuarioFacultadRol { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<CampoArticulo> CamposArticulos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<TipoUbicacion> TipoUbicacion { get; set; }

        public DbSet<ArticuloCampoValor> ArticuloCamposValores { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticuloCampoValor>()
               .HasOne(acv => acv.Articulo)
               .WithMany(a => a.CamposValores)
               .HasForeignKey(acv => acv.ArticuloId)
               .OnDelete(DeleteBehavior.Cascade); // Mantener CASCADE aquí

            modelBuilder.Entity<ArticuloCampoValor>()
                .HasOne(acv => acv.CampoArticulo)
                .WithMany(ca => ca.CamposValores)
                .HasForeignKey(acv => acv.CampoArticuloId)
                .OnDelete(DeleteBehavior.Restrict); // Evita cascada aquí
            modelBuilder.Entity<Traslado>()
                .HasOne(t => t.Articulo)
                .WithMany()
                .HasForeignKey(t => t.ArticuloId)
                .OnDelete(DeleteBehavior.Restrict); // No borrar traslados si borras el artículo
                                                    // Definir clave compuesta para evitar duplicados
            modelBuilder.Entity<UsuarioFacultadRol>()
                .HasKey(ufr => new { ufr.IdUsuario, ufr.IdFacultad, ufr.IdRol });

            modelBuilder.Entity<Traslado>()
                .HasOne(t => t.UbicacionOrigen)
                .WithMany()
                .HasForeignKey(t => t.UbicacionOrigenId)
                .OnDelete(DeleteBehavior.Restrict); // Evita el error de multiple cascade paths

            modelBuilder.Entity<Traslado>()
                .HasOne(t => t.UbicacionDestino)
                .WithMany()
                .HasForeignKey(t => t.UbicacionDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Traslado>()
                .HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}
