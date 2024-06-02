using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class promjenaKorisnickiRacunSaljiNovosti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "saljiNovosti",
                table: "Kupac");

            migrationBuilder.AddColumn<bool>(
                name: "saljiNovosti",
                table: "KorisnickiRacun",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "saljiNovosti",
                table: "KorisnickiRacun");

            migrationBuilder.AddColumn<bool>(
                name: "saljiNovosti",
                table: "Kupac",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
