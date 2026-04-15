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
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Ubicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Piso = table.Column<int>(type: "int", nullable: false),
                    TipoUbicacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ubicaciones_TipoUbicacion_TipoUbicacionId",
                        column: x => x.TipoUbicacionId,
                        principalTable: "TipoUbicacion",
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
                    SubModuloId = table.Column<int>(type: "int", nullable: false),
                    PermisoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermisos", x => x.Id);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QRCodeBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPatrimonial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaAdquision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorAdquisitivo = table.Column<double>(type: "float", nullable: false),
                    Condicion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vidaUtil = table.Column<int>(type: "int", nullable: false),
                    TipoArticuloId = table.Column<int>(type: "int", nullable: false),
                    UbicacionId = table.Column<int>(type: "int", nullable: true),
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
                name: "Prestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    NombreSolicitante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPrestamo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    EstadoPrestamo = table.Column<bool>(type: "bit", nullable: false)
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
                    { 8, 1, "fa-solid fa-shield-alt", "Seguridad", "/seguridad" }
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
                    { 1, 1, "Administrador" },
                    { 2, 1, "Usuario" }
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
                    { 5, 1, "fa-solid fa-user", 7, "Usuarios", "/usuarios" },
                    { 6, 1, "fa-solid fa-user-shield", 7, "Roles", "/roles" },
                    { 7, 1, "fa-solid fa-key", 8, "Permisos", "/permisos" },
                    { 8, 1, "fa-solid fa-layer-group", 7, "Modulos", "/modulos" },
                    { 9, 1, "fa-solid fa-handshake", 6, "Prestamos", "/prestamos" },
                    { 10, 1, "fa-solid fa-screwdriver-wrench", 7, "Mantenimiento", "/mantenimiento" }
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
                name: "IX_Prestamos_ArticuloId",
                table: "Prestamos",
                column: "ArticuloId");

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
                name: "IX_Ubicaciones_TipoUbicacionId",
                table: "Ubicaciones",
                column: "TipoUbicacionId");

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
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "RolPermisos");

            migrationBuilder.DropTable(
                name: "Traslado");

            migrationBuilder.DropTable(
                name: "CamposArticulos");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "SubModulos");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "TipoArticulos");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TipoUbicacion");
        }
    }
}
