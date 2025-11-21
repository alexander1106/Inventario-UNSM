using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class bdsistemainvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Icon",
                value: "fa-solid fa-box");

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Icon",
                value: "fa-solid fa-map-marker-alt");

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Icon",
                value: "fa-solid fa-exchange-alt");

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Icon",
                value: "fa-solid fa-warehouse");

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Icon",
                value: "fa-solid fa-chart-line");

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Icon",
                value: "fa-solid fa-shield-alt");

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Icon", "Ruta" },
                values: new object[] { "fa-solid fa-box-open", "/articulos" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Icon", "Ruta" },
                values: new object[] { "fa-solid fa-tags", "/tipos-articulos" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Icon", "Ruta" },
                values: new object[] { "fa-solid fa-map-marker", "/ubicaciones" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Icon", "Ruta" },
                values: new object[] { "fa-solid fa-layer-group", "/tipo-ubicacion" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Icon",
                value: "fa-solid fa-user");

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Icon",
                value: "fa-solid fa-user-shield");

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Icon",
                value: "fa-solid fa-key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Icon", "Ruta" },
                values: new object[] { null, "/articulos/lista" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Icon", "Ruta" },
                values: new object[] { null, "/articulos/tipos" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Icon", "Ruta" },
                values: new object[] { null, "/ubicaciones/lista" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Icon", "Ruta" },
                values: new object[] { null, "/ubicaciones/tipos" });

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Icon",
                value: null);
        }
    }
}
