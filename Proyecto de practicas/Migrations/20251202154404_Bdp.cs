using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class Bdp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-home", "Dashboard", "/dashboard" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-box", "Artículos", "/articulos" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-map-marker-alt", "Ubicaciones", "/ubicaciones" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-exchange-alt", "Traslados", "/traslados" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-warehouse", "Inventario", "/inventario" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-chart-line", "Reportes", "/reportes" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-shield-alt", "Seguridad", "/seguridad" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ModuloId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ModuloId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 3,
                column: "ModuloId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 4,
                column: "ModuloId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "ModuloId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 6,
                column: "ModuloId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 7,
                column: "ModuloId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 8,
                column: "ModuloId",
                value: 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-box", "Artículos", "/articulos" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-map-marker-alt", "Ubicaciones", "/ubicaciones" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-exchange-alt", "Traslados", "/traslados" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-warehouse", "Inventario", "/inventario" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-chart-line", "Reportes", "/reportes" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-shield-alt", "Seguridad", "/seguridad" });

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Icon", "Nombre", "Ruta" },
                values: new object[] { "fa-solid fa-home", "Dashboard", "/dashboard" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ModuloId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ModuloId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 3,
                column: "ModuloId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 4,
                column: "ModuloId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "ModuloId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 6,
                column: "ModuloId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 7,
                column: "ModuloId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 8,
                column: "ModuloId",
                value: 6);
        }
    }
}
