using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaBoletos.Migrations
{
    /// <inheritdoc />
    public partial class AddCedulaToPersona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cedula",
                table: "Personas",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "Personas");
        }
    }
}
