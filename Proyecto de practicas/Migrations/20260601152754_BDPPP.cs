using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class BDPPP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloCamposValores_articulos_ArticuloId",
                table: "ArticuloCamposValores");

            migrationBuilder.DropForeignKey(
                name: "FK_articulos_TipoArticulos_TipoArticuloId",
                table: "articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_articulos_Ubicaciones_UbicacionId",
                table: "articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Mantenimientos_articulos_ArticuloId",
                table: "Mantenimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_articulos_ArticuloId",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Traslado_articulos_ArticuloId",
                table: "Traslado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_articulos",
                table: "articulos");

            migrationBuilder.RenameTable(
                name: "articulos",
                newName: "Articulos");

            migrationBuilder.RenameIndex(
                name: "IX_articulos_UbicacionId",
                table: "Articulos",
                newName: "IX_Articulos_UbicacionId");

            migrationBuilder.RenameIndex(
                name: "IX_articulos_TipoArticuloId",
                table: "Articulos",
                newName: "IX_Articulos_TipoArticuloId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articulos",
                table: "Articulos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloCamposValores_Articulos_ArticuloId",
                table: "ArticuloCamposValores",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_TipoArticulos_TipoArticuloId",
                table: "Articulos",
                column: "TipoArticuloId",
                principalTable: "TipoArticulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Ubicaciones_UbicacionId",
                table: "Articulos",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mantenimientos_Articulos_ArticuloId",
                table: "Mantenimientos",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Articulos_ArticuloId",
                table: "Prestamos",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Traslado_Articulos_ArticuloId",
                table: "Traslado",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloCamposValores_Articulos_ArticuloId",
                table: "ArticuloCamposValores");

            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_TipoArticulos_TipoArticuloId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Ubicaciones_UbicacionId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Mantenimientos_Articulos_ArticuloId",
                table: "Mantenimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Articulos_ArticuloId",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Traslado_Articulos_ArticuloId",
                table: "Traslado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articulos",
                table: "Articulos");

            migrationBuilder.RenameTable(
                name: "Articulos",
                newName: "articulos");

            migrationBuilder.RenameIndex(
                name: "IX_Articulos_UbicacionId",
                table: "articulos",
                newName: "IX_articulos_UbicacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Articulos_TipoArticuloId",
                table: "articulos",
                newName: "IX_articulos_TipoArticuloId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_articulos",
                table: "articulos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloCamposValores_articulos_ArticuloId",
                table: "ArticuloCamposValores",
                column: "ArticuloId",
                principalTable: "articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_articulos_TipoArticulos_TipoArticuloId",
                table: "articulos",
                column: "TipoArticuloId",
                principalTable: "TipoArticulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_articulos_Ubicaciones_UbicacionId",
                table: "articulos",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mantenimientos_articulos_ArticuloId",
                table: "Mantenimientos",
                column: "ArticuloId",
                principalTable: "articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_articulos_ArticuloId",
                table: "Prestamos",
                column: "ArticuloId",
                principalTable: "articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Traslado_articulos_ArticuloId",
                table: "Traslado",
                column: "ArticuloId",
                principalTable: "articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
