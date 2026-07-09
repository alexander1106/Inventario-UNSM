using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Mantenimiento.Entity;
using Proyecto_de_practicas.Modules.Notificaciones.Entities;
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
        public DbSet<ClasificacionDepreciacion> ClasificacionesDepreciacion { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }

        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Solicitantes> Solicitantes { get; set; }
        public DbSet<Mantenimientos> Mantenimientos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        public DbSet<CampoArticulo> CamposArticulos { get; set; }
        public DbSet<ArticuloCampoValor> ArticuloCamposValores { get; set; }

        public DbSet<TipoUbicacion> TipoUbicacion { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Traslado> Traslado { get; set; }

        // Ubicacion
        public DbSet<Sedes> Sedes { get; set; }
        public DbSet<Facultades> Facultades { get; set; }
        public DbSet<Escuelas> Escuelas { get; set; }
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
                new Modulo { Id = 8, Nombre = "Seguridad", Ruta = "/seguridad", Icon = "fa-solid fa-shield-alt", Estado = 1 },
                new Modulo { Id = 9, Nombre = "Gestion institucional", Ruta = "/gestion-institucional", Icon = "fa-solid fa-shield-alt", Estado = 1 },
                new Modulo { Id = 10, Nombre = "Inventario", Ruta = "/inventario", Icon = "fa-solid fa-shield-alt", Estado = 1 }

            );
            modelBuilder.Entity<Articulo>().ToTable("Articulos");
            modelBuilder.Entity<Articulo>()
                .Property(a => a.ValorActual)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Articulo>()
                .HasOne(a => a.ClasificacionDepreciacion)
                .WithMany(c => c.Articulos)
                .HasForeignKey(a => a.ClasificacionDepreciacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Nombre = "superadmin", Estado = 1 },
                new Roles { Id = 2, Nombre = "Administrador", Estado = 1 },
                new Roles { Id = 3, Nombre = "Tecnico", Estado = 1 }
            );
            modelBuilder.Entity<Sedes>().HasData(
                new Sedes { Id = 1, Nombre = "Rioja", Direccion = "Jr. Santo Toribio N° 1200 ", Estado = true },
                new Sedes { Id = 2, Nombre = "Moyobamba", Direccion = "Prolongación 20 de Abril S/N (Cuadra 3, Barrio Calvario)", Estado = true },
                new Sedes { Id = 3, Nombre = "Lamas", Direccion = "Jr. Reynaldo Bartra S/N, Lamas", Estado = true },
                new Sedes { Id = 4, Nombre = "Morales", Direccion = " Jr. Amorarca N° 334, Morales", Estado = true }
                );
            modelBuilder.Entity<TipoArticulo>().HasData(
                new TipoArticulo { Id = 1, Nombre = "Equipo de computo personal", Descripcion = "Computadoras personales de escritorio, computadoras protales, computadores personales todo (AIO), Estaciones de trabajo, Thin Client, Tablets", Estado = 1, ImagenPath = "/" },
                new TipoArticulo { Id = 2, Nombre = "Impresoras & Escaner", Descripcion = "Sillas, mesas y mobiliario de oficina", Estado = 1, ImagenPath = "/" }
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
                new SubModulo { Id = 8, Nombre = "Modulos", Ruta = "/modulos", ModuloId = 8, Icon = "fa-solid fa-layer-group", Estado = 1 },
                new SubModulo { Id = 9, Nombre = "Sedes", Ruta = "/sedes", ModuloId = 3,  Icon = "fa-solid fa-shield-alt", Estado = 1 },
                new SubModulo { Id = 10, Nombre = "Facultades", Ruta = "/facultades", ModuloId = 3, Icon = "fa-solid fa-shield-alt", Estado = 1 },
                new SubModulo { Id = 11, Nombre = "Escuelas", Ruta = "/escuelas", ModuloId = 3, Icon = "fa-solid fa-shield-alt", Estado = 1 },
                new SubModulo { Id = 12, Nombre = "Prestamos", Ruta = "/prestamos", ModuloId = 5, Icon = "fa-solid fa-shield-alt", Estado = 1 }

            );


            // ==============================================================
            // ✨ NUEVO: TIPO ARTICULO SEED (Para Carga Masiva Automatizada)
            // ==============================================================
            modelBuilder.Entity<TipoArticulo>().HasData(
                // Asignamos Id = 100 por seguridad para que tus IDs locales/dinámicos no colisionen fácilmente
                new TipoArticulo { Id = 100, Nombre = "Otros",Descripcion = "Otros", Estado = 1 } 
            );

            // ==============================================================
            // TIPO UBICACION SEED
            // ==============================================================
            modelBuilder.Entity<TipoUbicacion>().HasData(
                new TipoUbicacion { Id = 1,   Nombre = "Aula",        Descripcion = "Aulas de clase presencial" },
                new TipoUbicacion { Id = 2,   Nombre = "Laboratorio", Descripcion = "Laboratorios de cómputo y ciencias" },
                new TipoUbicacion { Id = 100, Nombre = "General",     Descripcion = "Ubicación general sin clasificar" }
            );

            // ==============================================================
            // UBICACION SEED — Aulas
            // ==============================================================
            modelBuilder.Entity<Ubicacion>().HasData(
                // Aulas
                new Ubicacion { Id = 1,  Nombre = "Aula 101", Descripcion = "Aula de clases piso 1", Piso = 1, TipoUbicacionId = 1, EscuelaId = 1 },
                new Ubicacion { Id = 2,  Nombre = "Aula 102", Descripcion = "Aula de clases piso 1", Piso = 1, TipoUbicacionId = 1, EscuelaId = 1 },
                new Ubicacion { Id = 3,  Nombre = "Aula 103", Descripcion = "Aula de clases piso 1", Piso = 1, TipoUbicacionId = 1, EscuelaId = 1 },
                new Ubicacion { Id = 4,  Nombre = "Aula 201", Descripcion = "Aula de clases piso 2", Piso = 2, TipoUbicacionId = 1, EscuelaId = 1 },
                new Ubicacion { Id = 5,  Nombre = "Aula 202", Descripcion = "Aula de clases piso 2", Piso = 2, TipoUbicacionId = 1, EscuelaId = 1 },
                new Ubicacion { Id = 6,  Nombre = "Aula 203", Descripcion = "Aula de clases piso 2", Piso = 2, TipoUbicacionId = 1, EscuelaId = 2 },
                new Ubicacion { Id = 7,  Nombre = "Aula 301", Descripcion = "Aula de clases piso 3", Piso = 3, TipoUbicacionId = 1, EscuelaId = 2 },
                new Ubicacion { Id = 8,  Nombre = "Aula 302", Descripcion = "Aula de clases piso 3", Piso = 3, TipoUbicacionId = 1, EscuelaId = 2 },

                // Laboratorios
                new Ubicacion { Id = 9,  Nombre = "Laboratorio de Redes",          Descripcion = "Lab. de redes y comunicaciones",       Piso = 1, TipoUbicacionId = 2, EscuelaId = 1 },
                new Ubicacion { Id = 10, Nombre = "Laboratorio de Programación I",  Descripcion = "Lab. de programación básica",          Piso = 1, TipoUbicacionId = 2, EscuelaId = 1 },
                new Ubicacion { Id = 11, Nombre = "Laboratorio de Programación II", Descripcion = "Lab. de programación avanzada",        Piso = 2, TipoUbicacionId = 2, EscuelaId = 1 },
                new Ubicacion { Id = 12, Nombre = "Laboratorio de Base de Datos",   Descripcion = "Lab. de bases de datos y SQL",         Piso = 2, TipoUbicacionId = 2, EscuelaId = 1 },
                new Ubicacion { Id = 13, Nombre = "Laboratorio de Hardware",        Descripcion = "Lab. de mantenimiento de equipos",     Piso = 3, TipoUbicacionId = 2, EscuelaId = 1 },
                new Ubicacion { Id = 14, Nombre = "Laboratorio de Ciencias",        Descripcion = "Lab. de ciencias básicas",             Piso = 1, TipoUbicacionId = 2, EscuelaId = 3 },

                // General (por defecto para carga masiva)
                new Ubicacion { Id = 100, Nombre = "Otros", Descripcion = "Ubicación por defecto para artículos sin ubicación especificada", Piso = 0, TipoUbicacionId = 100 }
            );

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict); // 🔥 CLAVE


            modelBuilder.Entity<Permiso>().HasData(
                new Permiso { Id = 1, Nombre = "Crear", Activo = true },
                new Permiso { Id = 2, Nombre = "Editar", Activo = true },
                new Permiso { Id = 3, Nombre = "Ver", Activo = true },
                new Permiso { Id = 4, Nombre = "Eliminar", Activo = true }
            );

          
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
                new RolPermisos { Id = 37, RolId = 1, SubModuloId = 9,  PermisoId = 1 },
                new RolPermisos { Id = 38, RolId = 1, SubModuloId = 9,  PermisoId = 2 },
                new RolPermisos { Id = 39, RolId = 1, SubModuloId = 9,  PermisoId = 3 },
                new RolPermisos { Id = 40, RolId = 1, SubModuloId = 9,  PermisoId = 4 },
                new RolPermisos { Id = 41, RolId = 1, SubModuloId = 10, PermisoId = 1 },
                new RolPermisos { Id = 42, RolId = 1, SubModuloId = 10, PermisoId = 2 },
                new RolPermisos { Id = 43, RolId = 1, SubModuloId = 10, PermisoId = 3 },
                new RolPermisos { Id = 44, RolId = 1, SubModuloId = 10, PermisoId = 4 },
                new RolPermisos { Id = 45, RolId = 1, SubModuloId = 11, PermisoId = 1 },
                new RolPermisos { Id = 46, RolId = 1, SubModuloId = 11, PermisoId = 2 },
                new RolPermisos { Id = 47, RolId = 1, SubModuloId = 11, PermisoId = 3 },
                new RolPermisos { Id = 48, RolId = 1, SubModuloId = 11, PermisoId = 4 },
                new RolPermisos { Id = 49, RolId = 1, SubModuloId = 12, PermisoId = 1 },
                new RolPermisos { Id = 50, RolId = 1, SubModuloId = 12, PermisoId = 2 },
                new RolPermisos { Id = 51, RolId = 1, SubModuloId = 12, PermisoId = 3 },
                new RolPermisos { Id = 52, RolId = 1, SubModuloId = 12, PermisoId = 4 },

                // ADMINISTRADOR - Módulos sin submódulos (acceso total, excepto Dashboard)
                new RolPermisos { Id = 53, RolId = 1, ModuloId = 4, PermisoId = 1 },
                new RolPermisos { Id = 54, RolId = 1, ModuloId = 4, PermisoId = 2 },
                new RolPermisos { Id = 55, RolId = 1, ModuloId = 4, PermisoId = 3 },
                new RolPermisos { Id = 56, RolId = 1, ModuloId = 4, PermisoId = 4 },
                new RolPermisos { Id = 57, RolId = 1, ModuloId = 6, PermisoId = 1 },
                new RolPermisos { Id = 58, RolId = 1, ModuloId = 6, PermisoId = 2 },
                new RolPermisos { Id = 59, RolId = 1, ModuloId = 6, PermisoId = 3 },
                new RolPermisos { Id = 60, RolId = 1, ModuloId = 6, PermisoId = 4 },
                new RolPermisos { Id = 61, RolId = 1, ModuloId = 7, PermisoId = 1 },
                new RolPermisos { Id = 62, RolId = 1, ModuloId = 7, PermisoId = 2 },
                new RolPermisos { Id = 63, RolId = 1, ModuloId = 7, PermisoId = 3 },
                new RolPermisos { Id = 64, RolId = 1, ModuloId = 7, PermisoId = 4 },
                new RolPermisos { Id = 65, RolId = 1, ModuloId = 9, PermisoId = 1 },
                new RolPermisos { Id = 66, RolId = 1, ModuloId = 9, PermisoId = 2 },
                new RolPermisos { Id = 67, RolId = 1, ModuloId = 9, PermisoId = 3 },
                new RolPermisos { Id = 68, RolId = 1, ModuloId = 9, PermisoId = 4 },
                new RolPermisos { Id = 69, RolId = 1, ModuloId = 10, PermisoId = 1 },
                new RolPermisos { Id = 70, RolId = 1, ModuloId = 10, PermisoId = 2 },
                new RolPermisos { Id = 71, RolId = 1, ModuloId = 10, PermisoId = 3 },
                new RolPermisos { Id = 72, RolId = 1, ModuloId = 10, PermisoId = 4 },

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
         .HasOne(u => u.Escuela)
         .WithMany(e => e.Ubicaciones)
         .HasForeignKey(u => u.EscuelaId)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Escuelas>()
                .HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Escuelas>()
                .HasOne(e => e.Tecnico)
                .WithMany()
                .HasForeignKey(e => e.TecnicoId)
                .OnDelete(DeleteBehavior.Restrict);


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
            modelBuilder.Entity<Solicitantes>()
          .HasOne(s => s.Ubicacion)
          .WithMany(u => u.Solicitantes)
          .HasForeignKey(s => s.UbicacionId)
          .OnDelete(DeleteBehavior.Restrict);

            // ============================
            // FACULTADES SEED (UNSM - Tarapoto)
            // ============================
            modelBuilder.Entity<Facultades>().HasData(
                new Facultades { Id = 1, Nombre = "Facultad de Ingeniería de Sistemas e Informática", Direccion = "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", Estado = true, SedeId = 4 },
                new Facultades { Id = 2, Nombre = "Facultad de Ciencias Económicas", Direccion = "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", Estado = true, SedeId = 4 },
                new Facultades { Id = 3, Nombre = "Facultad de Ingeniería Agroindustrial", Direccion = "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", Estado = true, SedeId = 4 },
                new Facultades { Id = 4, Nombre = "Facultad de Educación y Humanidades", Direccion = "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", Estado = true, SedeId = 4 },
                new Facultades { Id = 5, Nombre = "Facultad de Ciencias Agrarias", Direccion = "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", Estado = true, SedeId = 4 },
                new Facultades { Id = 6, Nombre = "Facultad de Ciencias de la Salud", Direccion = "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", Estado = true, SedeId = 4 },
                new Facultades { Id = 7, Nombre = "Facultad de Derecho y Ciencias Políticas", Direccion = "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", Estado = true, SedeId = 4 },
                new Facultades { Id = 8, Nombre = "Facultad de Ecología", Direccion = "Prolongación 20 de Abril S/N (Cuadra 3, Barrio Calvario), Moyobamba", Estado = true, SedeId = 2 },
                new Facultades { Id = 9, Nombre = "Facultad de Ingeniería Civil y Arquitectura", Direccion = "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", Estado = true, SedeId = 4 },
                new Facultades { Id = 10, Nombre = "Facultad de Medicina Humana", Direccion = "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", Estado = true, SedeId = 4 }
            );

            // ============================
            // ESCUELAS SEED (UNSM - Tarapoto)
            // ============================
            modelBuilder.Entity<Escuelas>().HasData(
                new Escuelas { Id = 1, Nombre = "Ingeniería de Sistemas e Informática", FacultadId = 1 },
                new Escuelas { Id = 2, Nombre = "Contabilidad", FacultadId = 2 },
                new Escuelas { Id = 3, Nombre = "Administración", FacultadId = 2 },
                new Escuelas { Id = 4, Nombre = "Ingeniería Agroindustrial", FacultadId = 3 },
                new Escuelas { Id = 5, Nombre = "Educación Primaria", FacultadId = 4 },
                new Escuelas { Id = 6, Nombre = "Economía", FacultadId = 2 },
                new Escuelas { Id = 7, Nombre = "Turismo", FacultadId = 2 },
                new Escuelas { Id = 8, Nombre = "Educación Inicial", FacultadId = 4 },
                new Escuelas { Id = 9, Nombre = "Educación Secundaria", FacultadId = 4 },
                new Escuelas { Id = 10, Nombre = "Idiomas", FacultadId = 4 },
                new Escuelas { Id = 11, Nombre = "Psicología", FacultadId = 4 },
                new Escuelas { Id = 12, Nombre = "Agronomía", FacultadId = 5 },
                new Escuelas { Id = 13, Nombre = "Medicina Veterinaria", FacultadId = 5 },
                new Escuelas { Id = 14, Nombre = "Enfermería", FacultadId = 6 },
                new Escuelas { Id = 15, Nombre = "Obstetricia", FacultadId = 6 },
                new Escuelas { Id = 16, Nombre = "Derecho", FacultadId = 7 },
                new Escuelas { Id = 17, Nombre = "Ingeniería Ambiental", FacultadId = 8 },
                new Escuelas { Id = 18, Nombre = "Ingeniería Sanitaria", FacultadId = 8 },
                new Escuelas { Id = 19, Nombre = "Arquitectura", FacultadId = 9 },
                new Escuelas { Id = 20, Nombre = "Ingeniería Civil", FacultadId = 9 },
                new Escuelas { Id = 21, Nombre = "Medicina Humana", FacultadId = 10 }
            );

            // ============================
            // ARTICULOS SEED
            // ============================
            modelBuilder.Entity<Articulo>().HasData(
                new Articulo
                {
                    Id = 1,
                    CodigoPatrimonial = "UNSM-001",
                    Nombre = "Computadora de Escritorio HP",
                    FechaAdquision = new DateTime(2022, 3, 10),
                    ValorAdquisitivo = 2500.00,
                    Condicion = "Bueno",
                    TipoArticuloId = 1,
                    UbicacionId = 100,
                    Marca = "HP",
                    Modelo = "ProDesk 400 G7",
                    NroSerie = "SN-HP-001",
                    TiempoVidaUtil = 5,
                    DepreciacionAnual = 20,
                    ValorActual = 2000.00m,
                    Estado = 1
                },
                new Articulo
                {
                    Id = 2,
                    CodigoPatrimonial = "UNSM-002",
                    Nombre = "Silla Ergonómica",
                    FechaAdquision = new DateTime(2023, 6, 1),
                    ValorAdquisitivo = 350.00,
                    Condicion = "Bueno",
                    TipoArticuloId = 2,
                    UbicacionId = 100,
                    Marca = "Norditalia",
                    Modelo = "Ejecutiva-X",
                    TiempoVidaUtil = 10,
                    DepreciacionAnual = 10,
                    ValorActual = 315.00m,
                    Estado = 1
                }
            );
        }
    }
}