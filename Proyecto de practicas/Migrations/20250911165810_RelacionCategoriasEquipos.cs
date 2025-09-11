using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class RelacionCategoriasEquipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Equipos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCategoria",
                table: "Equipos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_CategoriaId",
                table: "Equipos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Categorias_CategoriaId",
                table: "Equipos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Categorias_CategoriaId",
                table: "Equipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_CategoriaId",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "Equipos");
        }
    }
}
