using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Security;
using Proyecto_de_practicas.Modules.Traslados.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options)
            : base(options) { }

        // ============================
        // 📦 DBSETS
        // ============================
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<SubModulo> SubModulos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolSubModulo> RolSubmodulo { get; set; }
        public DbSet<RolSubModuloPermiso> RolSubModuloPermisos { get; set; }

        public DbSet<TipoArticulo> TipoArticulos { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<CampoArticulo> CamposArticulos { get; set; }
        public DbSet<ArticuloCampoValor> ArticuloCamposValores { get; set; }

        public DbSet<TipoUbicacion> TipoUbicacion { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }

        public DbSet<Traslado> Traslado { get; set; }

        // ============================
        // 🔧 CONFIGURACIÓN
        // ============================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EncabezadoResult>().HasNoKey();

            // ============================
            // 🚀 SEED MODULOS
            // ============================
            modelBuilder.Entity<Modulo>().HasData(
                new Modulo { Id = 1, Nombre = "Dashboard", Ruta = "/dashboard", Icon = "fa-solid fa-home", Estado = 1 },
                new Modulo { Id = 2, Nombre = "Artículos", Ruta = "/articulos", Icon = "fa-solid fa-box", Estado = 1 },
                new Modulo { Id = 3, Nombre = "Ubicaciones", Ruta = "/ubicaciones", Icon = "fa-solid fa-map-marker-alt", Estado = 1 },
                new Modulo { Id = 4, Nombre = "Traslados", Ruta = "/traslados", Icon = "fa-solid fa-exchange-alt", Estado = 1 },
                new Modulo { Id = 6, Nombre = "Reportes", Ruta = "/reportes", Icon = "fa-solid fa-chart-line", Estado = 1 },
                new Modulo { Id = 7, Nombre = "Seguridad", Ruta = "/seguridad", Icon = "fa-solid fa-shield-alt", Estado = 1 }
            );

            // ============================
            // 🚀 ROLES
            // ============================
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Nombre = "Administrador", Estado = 1 },
                new Roles { Id = 2, Nombre = "Usuario", Estado = 1 }
            );

            // ============================
            // 🚀 SUBMODULOS
            // ============================
            modelBuilder.Entity<SubModulo>().HasData(
                new SubModulo { Id = 1, Nombre = "Artículos", Ruta = "/articulos", ModuloId = 2, Icon = "fa-solid fa-box-open", Estado = 1 },
                new SubModulo { Id = 2, Nombre = "Tipos de Artículo", Ruta = "/tipos-articulos", ModuloId = 2, Icon = "fa-solid fa-tags", Estado = 1 },

                new SubModulo { Id = 3, Nombre = "Ubicaciones", Ruta = "/ubicaciones", ModuloId = 3, Icon = "fa-solid fa-map-marker", Estado = 1 },
                new SubModulo { Id = 4, Nombre = "Tipos de Ubicación", Ruta = "/tipo-ubicacion", ModuloId = 3, Icon = "fa-solid fa-layer-group", Estado = 1 },

                new SubModulo { Id = 5, Nombre = "Usuarios", Ruta = "/usuarios", ModuloId = 7, Icon = "fa-solid fa-user", Estado = 1 },
                new SubModulo { Id = 6, Nombre = "Roles", Ruta = "/roles", ModuloId = 7, Icon = "fa-solid fa-user-shield", Estado = 1 },
                new SubModulo { Id = 7, Nombre = "Permisos", Ruta = "/permisos", ModuloId = 7, Icon = "fa-solid fa-key", Estado = 1 },
                new SubModulo { Id = 8, Nombre = "Modulos", Ruta = "/modulos", ModuloId = 7, Icon = "fa-solid fa-layer-group", Estado = 1 }
            );

            // ============================
            // 🚀 PERMISOS
            // ============================
            modelBuilder.Entity<Permiso>().HasData(
                new Permiso { Id = 1, Nombre = "Crear", Activo = true },
                new Permiso { Id = 2, Nombre = "Editar", Activo = true },
                new Permiso { Id = 3, Nombre = "Ver", Activo = true },
                new Permiso { Id = 4, Nombre = "Eliminar", Activo = true }
            );

            // ============================
            // 🔒 RELACIONES ARTICULOS
            // ============================
            modelBuilder.Entity<ArticuloCampoValor>()
                .HasOne(acv => acv.CampoArticulo)
                .WithMany(ca => ca.CamposValores)
                .HasForeignKey(acv => acv.CampoArticuloId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ArticuloCampoValor>()
                .HasOne(acv => acv.Articulo)
                .WithMany()
                .HasForeignKey(acv => acv.ArticuloId)
                .OnDelete(DeleteBehavior.Cascade);

            // ============================
            // 🔒 RELACIONES TRASLADOS (🔥 FIX CLAVE)
            // ============================
            modelBuilder.Entity<Traslado>(entity =>
            {
                entity.HasOne(t => t.Articulo)
                      .WithMany()
                      .HasForeignKey(t => t.ArticuloId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.Usuario)
                      .WithMany()
                      .HasForeignKey(t => t.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                // ❌ NO CASCADE → evita multiple cascade paths
                entity.HasOne(t => t.UbicacionOrigen)
                      .WithMany()
                      .HasForeignKey(t => t.UbicacionOrigenId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.UbicacionDestino)
                      .WithMany()
                      .HasForeignKey(t => t.UbicacionDestinoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
