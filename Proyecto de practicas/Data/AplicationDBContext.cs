using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;



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
        public DbSet<CampoArticulo> CamposArticulos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolSubModulo> RolSubmodulo { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Traslado> Traslado { get; set; }
        public DbSet<TipoArticulo> TipoArticulos{ get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<TipoUbicacion> TipoUbicacion { get; set; }
        public DbSet<ArticuloCampoValor> ArticuloCamposValores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Modulo>().HasData(
                new Modulo { Id = 1, Nombre = "Dashboard", Ruta = "/dashboard", Icon = "fa-solid fa-home" },
                new Modulo { Id = 2, Nombre = "Artículos", Ruta = "/articulos", Icon = "fa-solid fa-box" },
                new Modulo { Id = 3, Nombre = "Ubicaciones", Ruta = "/ubicaciones", Icon = "fa-solid fa-map-marker-alt" },
                new Modulo { Id = 4, Nombre = "Traslados", Ruta = "/traslados", Icon = "fa-solid fa-exchange-alt" },
                new Modulo { Id = 5, Nombre = "Inventario", Ruta = "/inventario", Icon = "fa-solid fa-warehouse" },
                new Modulo { Id = 6, Nombre = "Reportes", Ruta = "/reportes", Icon = "fa-solid fa-chart-line" },
                new Modulo { Id = 7, Nombre = "Seguridad", Ruta = "/seguridad", Icon = "fa-solid fa-shield-alt" }
            );

         
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Nombre = "Administrador",Estado=1},
                new Roles { Id = 2, Nombre = "Usuario", Estado=1}
            );

            // ============================ 
            // 🚀 SEED DE SUBMODULOS
            // ============================
            modelBuilder.Entity<SubModulo>().HasData(
              // ARTÍCULOS
              new SubModulo { Id = 1, Nombre = "Artículos", Ruta = "/articulos", ModuloId = 2, Icon = "fa-solid fa-box-open" },
              new SubModulo { Id = 2, Nombre = "Tipos de Artículo", Ruta = "/tipos-articulos", ModuloId = 2, Icon = "fa-solid fa-tags" },

              // UBICACIONES
              new SubModulo { Id = 3, Nombre = "Ubicaciones", Ruta = "/ubicaciones", ModuloId = 3, Icon = "fa-solid fa-map-marker" },
              new SubModulo { Id = 4, Nombre = "Tipos de Ubicación", Ruta = "/tipo-ubicacion", ModuloId = 3, Icon = "fa-solid fa-layer-group" },

              // SEGURIDAD
              new SubModulo { Id = 5, Nombre = "Usuarios", Ruta = "/usuarios", ModuloId = 7, Icon = "fa-solid fa-user" },
              new SubModulo { Id = 6, Nombre = "Roles", Ruta = "/roles", ModuloId = 7, Icon = "fa-solid fa-user-shield" },
              new SubModulo { Id = 7, Nombre = "Permisos", Ruta = "/permisos", ModuloId = 7, Icon = "fa-solid fa-key" },
              new SubModulo { Id = 8, Nombre = "Modulos", Ruta = "/modulos", ModuloId = 7, Icon = "fa-solid fa-layer-group" },

              // REPORTES
              new SubModulo { Id = 9, Nombre = "Reportes", Ruta = "/reportes", ModuloId = 6, Icon = "fa-solid fa-chart-line" }
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
                .HasOne(acv => acv.Articulo)
                .WithMany(a => a.CamposValores)
                .HasForeignKey(acv => acv.ArticuloId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArticuloCampoValor>()
                .HasOne(acv => acv.CampoArticulo)
                .WithMany(c => c.CamposValores)
                .HasForeignKey(acv => acv.CampoArticuloId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }

}
