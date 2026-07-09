using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class AddTecnicoToEscuelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TecnicoId",
                table: "Escuelas",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 1,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 2,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 3,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 4,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 5,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 6,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 7,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 8,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 9,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 10,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 11,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 12,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 13,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 14,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 15,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 16,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 17,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 18,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 19,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 20,
                column: "TecnicoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Escuelas",
                keyColumn: "Id",
                keyValue: 21,
                column: "TecnicoId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Escuelas_TecnicoId",
                table: "Escuelas",
                column: "TecnicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Escuelas_Usuarios_TecnicoId",
                table: "Escuelas",
                column: "TecnicoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escuelas_Usuarios_TecnicoId",
                table: "Escuelas");

            migrationBuilder.DropIndex(
                name: "IX_Escuelas_TecnicoId",
                table: "Escuelas");

            migrationBuilder.DropColumn(
                name: "TecnicoId",
                table: "Escuelas");
        }
    }
}
