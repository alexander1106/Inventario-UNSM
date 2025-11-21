using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class bdsistemainventI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Modulos",
                columns: new[] { "Id", "Estado", "Icon", "Nombre", "Ruta" },
                values: new object[] { 7, 1, "fa-solid fa-home", "Dashboard", "/dashboard" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
