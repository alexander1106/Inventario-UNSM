using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;



namespace Proyecto_de_practicas.Data
{
    public class AplicationDBContext: DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) { }
        public DbSet<Laboratorios> Laboratorios { get; set; }
        public DbSet<Herramientas> Herramientas { get; set; }
        public DbSet<Aulas> Aulas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles{ get; set; }
        public DbSet<Equipos> Equipos { get; set; }
        public DbSet<Pisos> Pisos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Facultades> Facultades { get; set; }
        
        public DbSet<Inventario> Inventario{ get; set; }
        public DbSet<Traslado> Traslado { get; set; }
       

        public DbSet<UsuarioFacultadRol> UsuarioFacultadRol { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
