using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class FixSubmoduloModulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SubModulos",
                columns: new[] { "Id", "Estado", "Icon", "ModuloId", "Nombre", "Ruta" },
                values: new object[] { 8, 1, "fa-solid fa-layer-group", 6, "Modulos", "/modulos" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubModulos",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
