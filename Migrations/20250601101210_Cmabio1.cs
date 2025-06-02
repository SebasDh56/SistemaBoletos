using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaBoletos.Migrations
{
    /// <inheritdoc />
    public partial class Cmabio1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoletosVendidos",
                table: "Cooperativas");

            migrationBuilder.RenameColumn(
                name: "CantidadBoletos",
                table: "Ventas",
                newName: "Cantidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Ventas",
                newName: "CantidadBoletos");

            migrationBuilder.AddColumn<int>(
                name: "BoletosVendidos",
                table: "Cooperativas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cooperativas",
                keyColumn: "Id",
                keyValue: 1,
                column: "BoletosVendidos",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Cooperativas",
                keyColumn: "Id",
                keyValue: 2,
                column: "BoletosVendidos",
                value: 0);
        }
    }
}
