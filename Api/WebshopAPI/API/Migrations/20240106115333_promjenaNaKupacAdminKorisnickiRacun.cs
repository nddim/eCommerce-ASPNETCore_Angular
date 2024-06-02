using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class promjenaNaKupacAdminKorisnickiRacun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Kupac");

            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "Kupac");

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "KorisnickiRacun",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "KorisnickiRacun",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "KorisnickiRacun");

            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "KorisnickiRacun");

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Kupac",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "Kupac",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
