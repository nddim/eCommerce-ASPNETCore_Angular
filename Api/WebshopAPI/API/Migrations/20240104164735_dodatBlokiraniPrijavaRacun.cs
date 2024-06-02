using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodatBlokiraniPrijavaRacun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlokiranaPrijavaRačun",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickiRacunID = table.Column<int>(type: "int", nullable: false),
                    VrijemeOdblokade = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlokiranaPrijavaRačun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlokiranaPrijavaRačun_KorisnickiRacun_KorisnickiRacunID",
                        column: x => x.KorisnickiRacunID,
                        principalTable: "KorisnickiRacun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlokiranaPrijavaRačun_KorisnickiRacunID",
                table: "BlokiranaPrijavaRačun",
                column: "KorisnickiRacunID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlokiranaPrijavaRačun");
        }
    }
}
