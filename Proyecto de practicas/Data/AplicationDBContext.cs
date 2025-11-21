using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Security;



namespace Proyecto_de_practicas.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) { }


        // Seguridad Entities
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<RolSubModuloPermiso> RolSubModuloPermisos { get; set; }
        public DbSet<SubModulo> SubModulos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolSubModulo> RolSubmodulo { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Traslado> Traslado { get; set; }
        public DbSet<TipoArticulo> TipoArticulos{ get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<CampoArticulo> CamposArticulos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<TipoUbicacion> TipoUbicacion { get; set; }
        public DbSet<ArticuloCampoValor> ArticuloCamposValores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Modulo>().HasData(
              new Modulo { Id = 1, Nombre = "Artículos", Ruta = "/articulos" },
              new Modulo { Id = 2, Nombre = "Ubicaciones", Ruta = "/ubicaciones" },
              new Modulo { Id = 3, Nombre = "Traslados", Ruta = "/traslados" },
              new Modulo { Id = 4, Nombre = "Inventario", Ruta = "/inventario" },
              new Modulo { Id = 5, Nombre = "Reportes", Ruta = "/reportes" },
              new Modulo { Id = 6, Nombre = "Seguridad", Ruta = "/seguridad" }
          );

            // =============================
            // 🚀 SEED DE ROLES
            // =============================
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Nombre = "Administrador",Estado=1},
                new Roles { Id = 2, Nombre = "Usuario", Estado=1}
            );
            
          // ============================ 
          // 🚀 SEED DE SUBMODULOS
          // ============================
          modelBuilder.Entity<SubModulo>().HasData(
            // ARTÍCULOS
            new SubModulo { Id = 1, Nombre = "Artículos", Ruta = "/articulos/lista", ModuloId = 1 },
            new SubModulo { Id = 2, Nombre = "Tipos de Artículo", Ruta = "/articulos/tipos", ModuloId = 1 },

            // UBICACIONES
            new SubModulo { Id = 3, Nombre = "Ubicaciones", Ruta = "/ubicaciones/lista", ModuloId = 2 },
            new SubModulo { Id = 4, Nombre = "Tipos de Ubicación", Ruta = "/ubicaciones/tipos", ModuloId = 2 },

            // SEGURIDAD
            new SubModulo { Id = 5, Nombre = "Usuarios", Ruta = "/seguridad/usuarios", ModuloId = 6 },
            new SubModulo { Id = 6, Nombre = "Roles", Ruta = "/seguridad/roles", ModuloId = 6 },
            new SubModulo { Id = 7, Nombre = "Permisos", Ruta = "/seguridad/permisos", ModuloId = 6 }
        );
            modelBuilder.Entity<Permiso>().HasData(
                new Permiso { Id = 1, Nombre = "Crear", Activo = true },
                new Permiso { Id = 2, Nombre = "Editar", Activo = true },
                new Permiso { Id = 3, Nombre = "Ver", Activo = true },
                new Permiso { Id = 4, Nombre = "Eliminar", Activo = true }
            );

            modelBuilder.Entity<Traslado>()
                .HasOne(t => t.UbicacionOrigen)
                .WithMany()
                .HasForeignKey(t => t.UbicacionOrigenId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Traslado>()
                .HasOne(t => t.UbicacionDestino)
                .WithMany()
                .HasForeignKey(t => t.UbicacionDestinoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Traslado>()
                .HasOne(t => t.Articulo)
                .WithMany()
                .HasForeignKey(t => t.ArticuloId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Traslado>()
                .HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);



            // 🔒 Relaciones para evitar cascade errors
            modelBuilder.Entity<ArticuloCampoValor>()
                .HasOne(acv => acv.CampoArticulo)
                .WithMany()
                .HasForeignKey(acv => acv.CampoArticuloId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ArticuloCampoValor>()
                .HasOne(acv => acv.Articulo)
                .WithMany()
                .HasForeignKey(acv => acv.ArticuloId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
