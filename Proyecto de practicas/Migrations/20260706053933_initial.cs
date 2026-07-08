using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncabezadoResult",
                columns: table => new
                {
                    Encabezado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoArticulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    ImagenPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoArticulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoUbicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUbicacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubModulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubModulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubModulos_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    ImagenPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facultades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    SedeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facultades_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CamposArticulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCampo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoArticuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CamposArticulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CamposArticulos_TipoArticulos_TipoArticuloId",
                        column: x => x.TipoArticuloId,
                        principalTable: "TipoArticulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolPermisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    ModuloId = table.Column<int>(type: "int", nullable: true),
                    SubModuloId = table.Column<int>(type: "int", nullable: true),
                    PermisoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolPermisos_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolPermisos_Permisos_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolPermisos_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolPermisos_SubModulos_SubModuloId",
                        column: x => x.SubModuloId,
                        principalTable: "SubModulos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Escuelas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacultadId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escuelas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escuelas_Facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Escuelas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Piso = table.Column<int>(type: "int", nullable: false),
                    TipoUbicacionId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    EscuelaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ubicaciones_Escuelas_EscuelaId",
                        column: x => x.EscuelaId,
                        principalTable: "Escuelas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ubicaciones_TipoUbicacion_TipoUbicacionId",
                        column: x => x.TipoUbicacionId,
                        principalTable: "TipoUbicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ubicaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPatrimonial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaAdquision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorAdquisitivo = table.Column<double>(type: "float", nullable: false),
                    Condicion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoArticuloId = table.Column<int>(type: "int", nullable: false),
                    UbicacionId = table.Column<int>(type: "int", nullable: true),
                    CodigoBarra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NroSerie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtrasObservaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TiempoVidaUtil = table.Column<double>(type: "float", nullable: false),
                    DepreciacionAnual = table.Column<double>(type: "float", nullable: false),
                    ValorActual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulos_TipoArticulos_TipoArticuloId",
                        column: x => x.TipoArticuloId,
                        principalTable: "TipoArticulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articulos_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Solicitantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciclo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UbicacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitantes_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticuloCamposValores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    CampoArticuloId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticuloCamposValores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticuloCamposValores_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticuloCamposValores_CamposArticulos_CampoArticuloId",
                        column: x => x.CampoArticuloId,
                        principalTable: "CamposArticulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    FechaMantenimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProveedorServicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false),
                    TipoMantenimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoMantenimiento = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Traslado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    UbicacionOrigenId = table.Column<int>(type: "int", nullable: false),
                    UbicacionDestinoId = table.Column<int>(type: "int", nullable: false),
                    FechaTraslado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traslado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Traslado_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Traslado_Ubicaciones_UbicacionDestinoId",
                        column: x => x.UbicacionDestinoId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Traslado_Ubicaciones_UbicacionOrigenId",
                        column: x => x.UbicacionOrigenId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Traslado_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    NombreSolicitante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPrestamo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    EstadoPrestamo = table.Column<bool>(type: "bit", nullable: false),
                    SolicitanteId = table.Column<int>(type: "int", nullable: false),
                    RutaPdf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aprobar = table.Column<bool>(type: "bit", nullable: false),
                    FirmadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFirma = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestamos_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prestamos_Solicitantes_SolicitanteId",
                        column: x => x.SolicitanteId,
                        principalTable: "Solicitantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Modulos",
                columns: new[] { "Id", "Estado", "Icon", "Nombre", "Ruta" },
                values: new object[,]
                {
                    { 1, 1, "fa-solid fa-home", "Dashboard", "/dashboard" },
                    { 2, 1, "fa-solid fa-box", "Artículos", "/articulos" },
                    { 3, 1, "fa-solid fa-map-marker-alt", "Ubicaciones", "/ubicaciones" },
                    { 4, 1, "fa-solid fa-exchange-alt", "Traslados", "/traslados" },
                    { 5, 1, "fa-solid fa-handshake", "Prestamos", "/prestamos" },
                    { 6, 1, "fa-solid fa-screwdriver-wrench", "Mantenimiento", "/mantenimiento" },
                    { 7, 1, "fa-solid fa-chart-line", "Reportes", "/reportes" },
                    { 8, 1, "fa-solid fa-shield-alt", "Seguridad", "/seguridad" },
                    { 9, 1, "fa-solid fa-shield-alt", "Gestion institucional", "/gestion-institucional" },
                    { 10, 1, "fa-solid fa-shield-alt", "Inventario", "/inventario" }
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "Id", "Activo", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "Crear" },
                    { 2, true, "Editar" },
                    { 3, true, "Ver" },
                    { 4, true, "Eliminar" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Estado", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "superadmin" },
                    { 2, 1, "Administrador" }
                });

            migrationBuilder.InsertData(
                table: "Sedes",
                columns: new[] { "Id", "Direccion", "Estado", "Nombre" },
                values: new object[,]
                {
                    { 1, "Jr. Santo Toribio N° 1200 ", true, "Rioja" },
                    { 2, "Prolongación 20 de Abril S/N (Cuadra 3, Barrio Calvario)", true, "Moyobamba" },
                    { 3, "Jr. Reynaldo Bartra S/N, Lamas", true, "Lamas" },
                    { 4, " Jr. Amorarca N° 334, Morales", true, "Morales" }
                });

            migrationBuilder.InsertData(
                table: "TipoArticulos",
                columns: new[] { "Id", "Descripcion", "Estado", "ImagenPath", "Nombre" },
                values: new object[,]
                {
                    { 1, "Computadoras personales de escritorio, computadoras protales, computadores personales todo (AIO), Estaciones de trabajo, Thin Client, Tablets", 1, "/", "Equipo de computo personal" },
                    { 2, "Sillas, mesas y mobiliario de oficina", 1, "/", "Impresoras & Escaner" },
                    { 100, "Otros", 1, null, "Otros" }
                });

            migrationBuilder.InsertData(
                table: "TipoUbicacion",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Aulas de clase presencial", "Aula" },
                    { 2, "Laboratorios de cómputo y ciencias", "Laboratorio" },
                    { 100, "Ubicación general sin clasificar", "General" }
                });

            migrationBuilder.InsertData(
                table: "Facultades",
                columns: new[] { "Id", "Direccion", "Estado", "Nombre", "SedeId" },
                values: new object[,]
                {
                    { 1, "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", true, "Facultad de Ingeniería de Sistemas e Informática", 4 },
                    { 2, "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", true, "Facultad de Ciencias Económicas", 4 },
                    { 3, "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", true, "Facultad de Ingeniería Agroindustrial", 4 },
                    { 4, "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", true, "Facultad de Educación y Humanidades", 4 },
                    { 5, "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", true, "Facultad de Ciencias Agrarias", 4 },
                    { 6, "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", true, "Facultad de Ciencias de la Salud", 4 },
                    { 7, "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", true, "Facultad de Derecho y Ciencias Políticas", 4 },
                    { 8, "Prolongación 20 de Abril S/N (Cuadra 3, Barrio Calvario), Moyobamba", true, "Facultad de Ecología", 2 },
                    { 9, "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", true, "Facultad de Ingeniería Civil y Arquitectura", 4 },
                    { 10, "Ciudad Universitaria, Jr. Amorarca N° 334, Morales", true, "Facultad de Medicina Humana", 4 }
                });

            migrationBuilder.InsertData(
                table: "RolPermisos",
                columns: new[] { "Id", "ModuloId", "PermisoId", "RolId", "SubModuloId" },
                values: new object[,]
                {
                    { 53, 4, 1, 1, null },
                    { 54, 4, 2, 1, null },
                    { 55, 4, 3, 1, null },
                    { 56, 4, 4, 1, null },
                    { 57, 6, 1, 1, null },
                    { 58, 6, 2, 1, null },
                    { 59, 6, 3, 1, null },
                    { 60, 6, 4, 1, null },
                    { 61, 7, 1, 1, null },
                    { 62, 7, 2, 1, null },
                    { 63, 7, 3, 1, null },
                    { 64, 7, 4, 1, null },
                    { 65, 9, 1, 1, null },
                    { 66, 9, 2, 1, null },
                    { 67, 9, 3, 1, null },
                    { 68, 9, 4, 1, null },
                    { 69, 10, 1, 1, null },
                    { 70, 10, 2, 1, null },
                    { 71, 10, 3, 1, null },
                    { 72, 10, 4, 1, null }
                });

            migrationBuilder.InsertData(
                table: "SubModulos",
                columns: new[] { "Id", "Estado", "Icon", "ModuloId", "Nombre", "Ruta" },
                values: new object[,]
                {
                    { 1, 1, "fa-solid fa-box-open", 2, "Artículos", "/articulos" },
                    { 2, 1, "fa-solid fa-tags", 2, "Tipos de Artículo", "/tipos-articulos" },
                    { 3, 1, "fa-solid fa-map-marker", 3, "Ubicaciones", "/ubicaciones" },
                    { 4, 1, "fa-solid fa-layer-group", 3, "Tipos de Ubicación", "/tipo-ubicacion" },
                    { 5, 1, "fa-solid fa-user", 8, "Usuarios", "/usuarios" },
                    { 6, 1, "fa-solid fa-user-shield", 8, "Roles", "/roles" },
                    { 7, 1, "fa-solid fa-key", 8, "Permisos", "/permisos" },
                    { 8, 1, "fa-solid fa-layer-group", 8, "Modulos", "/modulos" },
                    { 9, 1, "fa-solid fa-shield-alt", 3, "Sedes", "/sedes" },
                    { 10, 1, "fa-solid fa-shield-alt", 3, "Facultades", "/facultades" },
                    { 11, 1, "fa-solid fa-shield-alt", 3, "Escuelas", "/escuelas" },
                    { 12, 1, "fa-solid fa-shield-alt", 5, "Prestamos", "/prestamos" }
                });

            migrationBuilder.InsertData(
                table: "Ubicaciones",
                columns: new[] { "Id", "Descripcion", "EscuelaId", "Nombre", "Piso", "TipoUbicacionId", "UsuarioId" },
                values: new object[] { 100, "Ubicación por defecto para artículos sin ubicación especificada", null, "Otros", 0, 100, null });

            migrationBuilder.InsertData(
                table: "Articulos",
                columns: new[] { "Id", "CodigoBarra", "CodigoPatrimonial", "Condicion", "DepreciacionAnual", "Estado", "FechaAdquision", "Marca", "Modelo", "Nombre", "NroSerie", "OtrasObservaciones", "TiempoVidaUtil", "TipoArticuloId", "UbicacionId", "ValorActual", "ValorAdquisitivo" },
                values: new object[,]
                {
                    { 1, null, "UNSM-001", "Bueno", 20.0, 1, new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "HP", "ProDesk 400 G7", "Computadora de Escritorio HP", "SN-HP-001", null, 5.0, 1, 100, 2000.00m, 2500.0 },
                    { 2, null, "UNSM-002", "Bueno", 10.0, 1, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Norditalia", "Ejecutiva-X", "Silla Ergonómica", null, null, 10.0, 2, 100, 315.00m, 350.0 }
                });

            migrationBuilder.InsertData(
                table: "Escuelas",
                columns: new[] { "Id", "FacultadId", "ImagenUrl", "Nombre", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, null, "Ingeniería de Sistemas e Informática", null },
                    { 2, 2, null, "Contabilidad", null },
                    { 3, 2, null, "Administración", null },
                    { 4, 3, null, "Ingeniería Agroindustrial", null },
                    { 5, 4, null, "Educación Primaria", null },
                    { 6, 2, null, "Economía", null },
                    { 7, 2, null, "Turismo", null },
                    { 8, 4, null, "Educación Inicial", null },
                    { 9, 4, null, "Educación Secundaria", null },
                    { 10, 4, null, "Idiomas", null },
                    { 11, 4, null, "Psicología", null },
                    { 12, 5, null, "Agronomía", null },
                    { 13, 5, null, "Medicina Veterinaria", null },
                    { 14, 6, null, "Enfermería", null },
                    { 15, 6, null, "Obstetricia", null },
                    { 16, 7, null, "Derecho", null },
                    { 17, 8, null, "Ingeniería Ambiental", null },
                    { 18, 8, null, "Ingeniería Sanitaria", null },
                    { 19, 9, null, "Arquitectura", null },
                    { 20, 9, null, "Ingeniería Civil", null },
                    { 21, 10, null, "Medicina Humana", null }
                });

            migrationBuilder.InsertData(
                table: "RolPermisos",
                columns: new[] { "Id", "ModuloId", "PermisoId", "RolId", "SubModuloId" },
                values: new object[,]
                {
                    { 1, null, 1, 1, 1 },
                    { 2, null, 2, 1, 1 },
                    { 3, null, 3, 1, 1 },
                    { 4, null, 4, 1, 1 },
                    { 5, null, 1, 1, 2 },
                    { 6, null, 2, 1, 2 },
                    { 7, null, 3, 1, 2 },
                    { 8, null, 4, 1, 2 },
                    { 9, null, 1, 1, 3 },
                    { 10, null, 2, 1, 3 },
                    { 11, null, 3, 1, 3 },
                    { 12, null, 4, 1, 3 },
                    { 13, null, 1, 1, 4 },
                    { 14, null, 2, 1, 4 },
                    { 15, null, 3, 1, 4 },
                    { 16, null, 4, 1, 4 },
                    { 17, null, 1, 1, 5 },
                    { 18, null, 2, 1, 5 },
                    { 19, null, 3, 1, 5 },
                    { 20, null, 4, 1, 5 },
                    { 21, null, 1, 1, 6 },
                    { 22, null, 2, 1, 6 },
                    { 23, null, 3, 1, 6 },
                    { 24, null, 4, 1, 6 },
                    { 25, null, 1, 1, 7 },
                    { 26, null, 2, 1, 7 },
                    { 27, null, 3, 1, 7 },
                    { 28, null, 4, 1, 7 },
                    { 29, null, 1, 1, 8 },
                    { 30, null, 2, 1, 8 },
                    { 31, null, 3, 1, 8 },
                    { 32, null, 4, 1, 8 },
                    { 33, null, 3, 2, 1 },
                    { 34, null, 3, 2, 2 },
                    { 35, null, 3, 2, 3 },
                    { 36, null, 3, 2, 4 },
                    { 37, null, 1, 1, 9 },
                    { 38, null, 2, 1, 9 },
                    { 39, null, 3, 1, 9 },
                    { 40, null, 4, 1, 9 },
                    { 41, null, 1, 1, 10 },
                    { 42, null, 2, 1, 10 },
                    { 43, null, 3, 1, 10 },
                    { 44, null, 4, 1, 10 },
                    { 45, null, 1, 1, 11 },
                    { 46, null, 2, 1, 11 },
                    { 47, null, 3, 1, 11 },
                    { 48, null, 4, 1, 11 },
                    { 49, null, 1, 1, 12 },
                    { 50, null, 2, 1, 12 },
                    { 51, null, 3, 1, 12 },
                    { 52, null, 4, 1, 12 }
                });

            migrationBuilder.InsertData(
                table: "Ubicaciones",
                columns: new[] { "Id", "Descripcion", "EscuelaId", "Nombre", "Piso", "TipoUbicacionId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Aula de clases piso 1", 1, "Aula 101", 1, 1, null },
                    { 2, "Aula de clases piso 1", 1, "Aula 102", 1, 1, null },
                    { 3, "Aula de clases piso 1", 1, "Aula 103", 1, 1, null },
                    { 4, "Aula de clases piso 2", 1, "Aula 201", 2, 1, null },
                    { 5, "Aula de clases piso 2", 1, "Aula 202", 2, 1, null },
                    { 6, "Aula de clases piso 2", 2, "Aula 203", 2, 1, null },
                    { 7, "Aula de clases piso 3", 2, "Aula 301", 3, 1, null },
                    { 8, "Aula de clases piso 3", 2, "Aula 302", 3, 1, null },
                    { 9, "Lab. de redes y comunicaciones", 1, "Laboratorio de Redes", 1, 2, null },
                    { 10, "Lab. de programación básica", 1, "Laboratorio de Programación I", 1, 2, null },
                    { 11, "Lab. de programación avanzada", 1, "Laboratorio de Programación II", 2, 2, null },
                    { 12, "Lab. de bases de datos y SQL", 1, "Laboratorio de Base de Datos", 2, 2, null },
                    { 13, "Lab. de mantenimiento de equipos", 1, "Laboratorio de Hardware", 3, 2, null },
                    { 14, "Lab. de ciencias básicas", 3, "Laboratorio de Ciencias", 1, 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloCamposValores_ArticuloId",
                table: "ArticuloCamposValores",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloCamposValores_CampoArticuloId",
                table: "ArticuloCamposValores",
                column: "CampoArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_TipoArticuloId",
                table: "Articulos",
                column: "TipoArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_UbicacionId",
                table: "Articulos",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_CamposArticulos_TipoArticuloId",
                table: "CamposArticulos",
                column: "TipoArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Escuelas_FacultadId",
                table: "Escuelas",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_Escuelas_UsuarioId",
                table: "Escuelas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Facultades_SedeId",
                table: "Facultades",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_ArticuloId",
                table: "Mantenimientos",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_ArticuloId",
                table: "Prestamos",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_SolicitanteId",
                table: "Prestamos",
                column: "SolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_ModuloId",
                table: "RolPermisos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_PermisoId",
                table: "RolPermisos",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_RolId",
                table: "RolPermisos",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_SubModuloId",
                table: "RolPermisos",
                column: "SubModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitantes_UbicacionId",
                table: "Solicitantes",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubModulos_ModuloId",
                table: "SubModulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslado_ArticuloId",
                table: "Traslado",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslado_UbicacionDestinoId",
                table: "Traslado",
                column: "UbicacionDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslado_UbicacionOrigenId",
                table: "Traslado",
                column: "UbicacionOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslado_UsuarioId",
                table: "Traslado",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_EscuelaId",
                table: "Ubicaciones",
                column: "EscuelaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_TipoUbicacionId",
                table: "Ubicaciones",
                column: "TipoUbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_UsuarioId",
                table: "Ubicaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticuloCamposValores");

            migrationBuilder.DropTable(
                name: "EncabezadoResult");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "RolPermisos");

            migrationBuilder.DropTable(
                name: "Traslado");

            migrationBuilder.DropTable(
                name: "CamposArticulos");

            migrationBuilder.DropTable(
                name: "Solicitantes");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "SubModulos");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "TipoArticulos");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Escuelas");

            migrationBuilder.DropTable(
                name: "TipoUbicacion");

            migrationBuilder.DropTable(
                name: "Facultades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
