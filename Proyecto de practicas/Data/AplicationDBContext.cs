using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Mantenimiento.Entity;
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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<SubModulo> SubModulos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermisos> RolPermisos { get; set; }

        public DbSet<TipoArticulo> TipoArticulos { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }

        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<Mantenimientos> Mantenimientos { get; set; }

        public DbSet<CampoArticulo> CamposArticulos { get; set; }
        public DbSet<ArticuloCampoValor> ArticuloCamposValores { get; set; }

        public DbSet<TipoUbicacion> TipoUbicacion { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Traslado> Traslado { get; set; }
        public object Articulos { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EncabezadoResult>().HasNoKey();

            // ============================
            // 🚀 MODULOS SEED
            // ============================
            modelBuilder.Entity<Modulo>().HasData(
                new Modulo { Id = 1, Nombre = "Dashboard", Ruta = "/dashboard", Icon = "fa-solid fa-home", Estado = 1 },
                new Modulo { Id = 2, Nombre = "Artículos", Ruta = "/articulos", Icon = "fa-solid fa-box", Estado = 1 },
                new Modulo { Id = 3, Nombre = "Ubicaciones", Ruta = "/ubicaciones", Icon = "fa-solid fa-map-marker-alt", Estado = 1 },
                new Modulo { Id = 4, Nombre = "Traslados", Ruta = "/traslados", Icon = "fa-solid fa-exchange-alt", Estado = 1 },
                new Modulo { Id = 5, Nombre = "Prestamos", Ruta = "/prestamos", Icon = "fa-solid fa-handshake", Estado = 1 },
                new Modulo { Id = 6, Nombre = "Mantenimiento", Ruta = "/mantenimiento", Icon = "fa-solid fa-screwdriver-wrench", Estado = 1 },
                new Modulo { Id = 7, Nombre = "Reportes", Ruta = "/reportes", Icon = "fa-solid fa-chart-line", Estado = 1 },
                new Modulo { Id = 8, Nombre = "Seguridad", Ruta = "/seguridad", Icon = "fa-solid fa-shield-alt", Estado = 1 }
            );

            // ============================
            // 🚀 ROLES SEED
            // ============================
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Nombre = "Administrador", Estado = 1 },
                new Roles { Id = 2, Nombre = "Usuario", Estado = 1 }
            );

            // ============================
            // 🚀 SUBMODULOS SEED
            // ============================
            modelBuilder.Entity<SubModulo>().HasData(
                new SubModulo { Id = 1, Nombre = "Artículos", Ruta = "/articulos", ModuloId = 2, Icon = "fa-solid fa-box-open", Estado = 1 },
                new SubModulo { Id = 2, Nombre = "Tipos de Artículo", Ruta = "/tipos-articulos", ModuloId = 2, Icon = "fa-solid fa-tags", Estado = 1 },
                new SubModulo { Id = 3, Nombre = "Ubicaciones", Ruta = "/ubicaciones", ModuloId = 3, Icon = "fa-solid fa-map-marker", Estado = 1 },
                new SubModulo { Id = 4, Nombre = "Tipos de Ubicación", Ruta = "/tipo-ubicacion", ModuloId = 3, Icon = "fa-solid fa-layer-group", Estado = 1 },
                new SubModulo { Id = 5, Nombre = "Usuarios", Ruta = "/usuarios", ModuloId = 8, Icon = "fa-solid fa-user", Estado = 1 },
                new SubModulo { Id = 6, Nombre = "Roles", Ruta = "/roles", ModuloId = 8, Icon = "fa-solid fa-user-shield", Estado = 1 },
                new SubModulo { Id = 7, Nombre = "Permisos", Ruta = "/permisos", ModuloId = 8, Icon = "fa-solid fa-key", Estado = 1 },
                new SubModulo { Id = 8, Nombre = "Modulos", Ruta = "/modulos", ModuloId = 8, Icon = "fa-solid fa-layer-group", Estado = 1 }
            );

            // ==============================================================
            // ✨ NUEVO: TIPO ARTICULO SEED (Para Carga Masiva Automatizada)
            // ==============================================================
            modelBuilder.Entity<TipoArticulo>().HasData(
                // Asignamos Id = 100 por seguridad para que tus IDs locales/dinámicos no colisionen fácilmente
                new TipoArticulo { Id = 100, Nombre = "Otros",Descripcion = "Otros", Estado = 1 } 
            );

            // ==============================================================
            // ✨ NUEVO: TIPO UBICACION SEED (Obligatorio para la FK de Ubicación)
            // ==============================================================
            modelBuilder.Entity<TipoUbicacion>().HasData(
                new TipoUbicacion
                {
                    Id = 100,
                    Nombre = "General",
                    Descripcion = "General"
                }
            );

            // ==============================================================
            // ✨ NUEVO: UBICACION SEED (Para Carga Masiva Automatizada)
            // ==============================================================
            modelBuilder.Entity<Ubicacion>().HasData(
                new Ubicacion
                {
                    Id = 100,
                    Nombre = "Otros",
                    Descripcion = "Ubicación por defecto para artículos sin ubicación especificada",
                    Piso = 0,
                    TipoUbicacionId = 100,
                    ImagenUrl = null,
                    UsuarioId = null,
                    PadreId = null
                    // 💡 No inicialices "Articulos" ni "Hijos" aquí, HasData se encarga de las propiedades primitivas.
                }
            );

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Permiso>().HasData(
                new Permiso { Id = 1, Nombre = "Crear", Activo = true },
                new Permiso { Id = 2, Nombre = "Editar", Activo = true },
                new Permiso { Id = 3, Nombre = "Ver", Activo = true },
                new Permiso { Id = 4, Nombre = "Eliminar", Activo = true }
            );

            // ============================
            // 🔐 ROL PERMISOS SEED
            // ============================
            modelBuilder.Entity<RolPermisos>().HasData(
                // ADMINISTRADOR
                new RolPermisos { Id = 1, RolId = 1, SubModuloId = 1, PermisoId = 1 },
                new RolPermisos { Id = 2, RolId = 1, SubModuloId = 1, PermisoId = 2 },
                new RolPermisos { Id = 3, RolId = 1, SubModuloId = 1, PermisoId = 3 },
                new RolPermisos { Id = 4, RolId = 1, SubModuloId = 1, PermisoId = 4 },
                new RolPermisos { Id = 5, RolId = 1, SubModuloId = 2, PermisoId = 1 },
                new RolPermisos { Id = 6, RolId = 1, SubModuloId = 2, PermisoId = 2 },
                new RolPermisos { Id = 7, RolId = 1, SubModuloId = 2, PermisoId = 3 },
                new RolPermisos { Id = 8, RolId = 1, SubModuloId = 2, PermisoId = 4 },
                new RolPermisos { Id = 9, RolId = 1, SubModuloId = 3, PermisoId = 1 },
                new RolPermisos { Id = 10, RolId = 1, SubModuloId = 3, PermisoId = 2 },
                new RolPermisos { Id = 11, RolId = 1, SubModuloId = 3, PermisoId = 3 },
                new RolPermisos { Id = 12, RolId = 1, SubModuloId = 3, PermisoId = 4 },
                new RolPermisos { Id = 13, RolId = 1, SubModuloId = 4, PermisoId = 1 },
                new RolPermisos { Id = 14, RolId = 1, SubModuloId = 4, PermisoId = 2 },
                new RolPermisos { Id = 15, RolId = 1, SubModuloId = 4, PermisoId = 3 },
                new RolPermisos { Id = 16, RolId = 1, SubModuloId = 4, PermisoId = 4 },
                new RolPermisos { Id = 17, RolId = 1, SubModuloId = 5, PermisoId = 1 },
                new RolPermisos { Id = 18, RolId = 1, SubModuloId = 5, PermisoId = 2 },
                new RolPermisos { Id = 19, RolId = 1, SubModuloId = 5, PermisoId = 3 },
                new RolPermisos { Id = 20, RolId = 1, SubModuloId = 5, PermisoId = 4 },
                new RolPermisos { Id = 21, RolId = 1, SubModuloId = 6, PermisoId = 1 },
                new RolPermisos { Id = 22, RolId = 1, SubModuloId = 6, PermisoId = 2 },
                new RolPermisos { Id = 23, RolId = 1, SubModuloId = 6, PermisoId = 3 },
                new RolPermisos { Id = 24, RolId = 1, SubModuloId = 6, PermisoId = 4 },
                new RolPermisos { Id = 25, RolId = 1, SubModuloId = 7, PermisoId = 1 },
                new RolPermisos { Id = 26, RolId = 1, SubModuloId = 7, PermisoId = 2 },
                new RolPermisos { Id = 27, RolId = 1, SubModuloId = 7, PermisoId = 3 },
                new RolPermisos { Id = 28, RolId = 1, SubModuloId = 7, PermisoId = 4 },
                new RolPermisos { Id = 29, RolId = 1, SubModuloId = 8, PermisoId = 1 },
                new RolPermisos { Id = 30, RolId = 1, SubModuloId = 8, PermisoId = 2 },
                new RolPermisos { Id = 31, RolId = 1, SubModuloId = 8, PermisoId = 3 },
                new RolPermisos { Id = 32, RolId = 1, SubModuloId = 8, PermisoId = 4 },

                // USUARIO
                new RolPermisos { Id = 33, RolId = 2, SubModuloId = 1, PermisoId = 3 },
                new RolPermisos { Id = 34, RolId = 2, SubModuloId = 2, PermisoId = 3 },
                new RolPermisos { Id = 35, RolId = 2, SubModuloId = 3, PermisoId = 3 },
                new RolPermisos { Id = 36, RolId = 2, SubModuloId = 4, PermisoId = 3 }
            );

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

            modelBuilder.Entity<Ubicacion>()
                .HasOne(u => u.Usuario)
                .WithMany()
                .HasForeignKey(u => u.UsuarioId)
                .IsRequired(false);

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