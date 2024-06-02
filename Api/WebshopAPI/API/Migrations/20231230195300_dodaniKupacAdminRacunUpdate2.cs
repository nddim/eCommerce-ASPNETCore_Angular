using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodaniKupacAdminRacunUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administratori_KorisnickiRacun_Id",
                table: "Administratori");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administratori",
                table: "Administratori");

            migrationBuilder.RenameTable(
                name: "Administratori",
                newName: "Administrator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_KorisnickiRacun_Id",
                table: "Administrator",
                column: "Id",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_KorisnickiRacun_Id",
                table: "Administrator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator");

            migrationBuilder.RenameTable(
                name: "Administrator",
                newName: "Administratori");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administratori",
                table: "Administratori",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Administratori_KorisnickiRacun_Id",
                table: "Administratori",
                column: "Id",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
