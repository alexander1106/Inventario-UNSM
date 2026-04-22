using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class BdPRACTICAS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolPermisos_Permisos_PermisoId",
                table: "RolPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_RolPermisos_SubModulos_SubModuloId",
                table: "RolPermisos");

            migrationBuilder.AddColumn<int>(
                name: "PermisoId1",
                table: "RolPermisos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_PermisoId1",
                table: "RolPermisos",
                column: "PermisoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RolPermisos_Permisos_PermisoId",
                table: "RolPermisos",
                column: "PermisoId",
                principalTable: "Permisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolPermisos_Permisos_PermisoId1",
                table: "RolPermisos",
                column: "PermisoId1",
                principalTable: "Permisos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolPermisos_SubModulos_SubModuloId",
                table: "RolPermisos",
                column: "SubModuloId",
                principalTable: "SubModulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolPermisos_Permisos_PermisoId",
                table: "RolPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_RolPermisos_Permisos_PermisoId1",
                table: "RolPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_RolPermisos_SubModulos_SubModuloId",
                table: "RolPermisos");

            migrationBuilder.DropIndex(
                name: "IX_RolPermisos_PermisoId1",
                table: "RolPermisos");

            migrationBuilder.DropColumn(
                name: "PermisoId1",
                table: "RolPermisos");

            migrationBuilder.AddForeignKey(
                name: "FK_RolPermisos_Permisos_PermisoId",
                table: "RolPermisos",
                column: "PermisoId",
                principalTable: "Permisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolPermisos_SubModulos_SubModuloId",
                table: "RolPermisos",
                column: "SubModuloId",
                principalTable: "SubModulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
