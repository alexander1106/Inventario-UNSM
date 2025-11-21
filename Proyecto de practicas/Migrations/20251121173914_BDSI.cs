using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class BDSI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Ruta",
                value: "/usuarios");

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Ruta",
                value: "/roles");

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Ruta",
                value: "/permisos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Ruta",
                value: "/seguridad/usuarios");

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Ruta",
                value: "/seguridad/roles");

            migrationBuilder.UpdateData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Ruta",
                value: "/seguridad/permisos");
        }
    }
}
