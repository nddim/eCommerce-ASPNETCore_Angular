using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodaniKupacAdminRacunUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administratori_KorisnickiRacuni_Id",
                table: "Administratori");

            migrationBuilder.DropForeignKey(
                name: "FK_Kupac_KorisnickiRacuni_Id",
                table: "Kupac");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KorisnickiRacuni",
                table: "KorisnickiRacuni");

            migrationBuilder.RenameTable(
                name: "KorisnickiRacuni",
                newName: "KorisnickiRacun");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KorisnickiRacun",
                table: "KorisnickiRacun",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Administratori_KorisnickiRacun_Id",
                table: "Administratori",
                column: "Id",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kupac_KorisnickiRacun_Id",
                table: "Kupac",
                column: "Id",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administratori_KorisnickiRacun_Id",
                table: "Administratori");

            migrationBuilder.DropForeignKey(
                name: "FK_Kupac_KorisnickiRacun_Id",
                table: "Kupac");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KorisnickiRacun",
                table: "KorisnickiRacun");

            migrationBuilder.RenameTable(
                name: "KorisnickiRacun",
                newName: "KorisnickiRacuni");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KorisnickiRacuni",
                table: "KorisnickiRacuni",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Administratori_KorisnickiRacuni_Id",
                table: "Administratori",
                column: "Id",
                principalTable: "KorisnickiRacuni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kupac_KorisnickiRacuni_Id",
                table: "Kupac",
                column: "Id",
                principalTable: "KorisnickiRacuni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
