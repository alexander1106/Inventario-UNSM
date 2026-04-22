using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "ModuloId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 6,
                column: "ModuloId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 8,
                column: "ModuloId",
                value: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 8,
                column: "ModuloId",
                value: 7);

            migrationBuilder.InsertData(
                table: "SubModulos",
                columns: new[] { "Id", "Estado", "Icon", "ModuloId", "Nombre", "Ruta" },
                values: new object[,]
                {
                    { 9, 1, "fa-solid fa-handshake", 6, "Prestamos", "/prestamos" },
                    { 10, 1, "fa-solid fa-screwdriver-wrench", 7, "Mantenimiento", "/mantenimiento" }
                });
        }
    }
}
