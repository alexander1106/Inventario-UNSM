using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_de_practicas.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentoFirmaToTraslado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFirma",
                table: "Traslado",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmadoPor",
                table: "Traslado",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaPdf",
                table: "Traslado",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFirma",
                table: "Traslado");

            migrationBuilder.DropColumn(
                name: "FirmadoPor",
                table: "Traslado");

            migrationBuilder.DropColumn(
                name: "RutaPdf",
                table: "Traslado");
        }
    }
}
