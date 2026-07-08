using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class AddClasificacionDepreciacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClasificacionDepreciacionId",
                table: "Articulos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClasificacionesDepreciacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VidaUtilAnios = table.Column<double>(type: "float", nullable: false),
                    PorcentajeDepreciacionAnual = table.Column<double>(type: "float", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasificacionesDepreciacion", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClasificacionDepreciacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClasificacionDepreciacionId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_ClasificacionDepreciacionId",
                table: "Articulos",
                column: "ClasificacionDepreciacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_ClasificacionesDepreciacion_ClasificacionDepreciacionId",
                table: "Articulos",
                column: "ClasificacionDepreciacionId",
                principalTable: "ClasificacionesDepreciacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ClasificacionesDepreciacion_ClasificacionDepreciacionId",
                table: "Articulos");

            migrationBuilder.DropTable(
                name: "ClasificacionesDepreciacion");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_ClasificacionDepreciacionId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "ClasificacionDepreciacionId",
                table: "Articulos");
        }
    }
}
