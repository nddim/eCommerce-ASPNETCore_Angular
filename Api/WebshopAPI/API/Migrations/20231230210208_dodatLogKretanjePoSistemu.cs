using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodatLogKretanjePoSistemu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is2FActive",
                table: "KorisnickiRacun",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isAdmin",
                table: "KorisnickiRacun",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isKupac",
                table: "KorisnickiRacun",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AutentifikacijaToken",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vrijednost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnickiRacunId = table.Column<int>(type: "int", nullable: false),
                    vrijemeEvidentiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ipAdresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is2FOtkljucano = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutentifikacijaToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_AutentifikacijaToken_KorisnickiRacun_KorisnickiRacunId",
                        column: x => x.KorisnickiRacunId,
                        principalTable: "KorisnickiRacun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutentifikacijaToken_KorisnickiRacunId",
                table: "AutentifikacijaToken",
                column: "KorisnickiRacunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutentifikacijaToken");

            migrationBuilder.DropColumn(
                name: "Is2FActive",
                table: "KorisnickiRacun");

            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "KorisnickiRacun");

            migrationBuilder.DropColumn(
                name: "isKupac",
                table: "KorisnickiRacun");
        }
    }
}
