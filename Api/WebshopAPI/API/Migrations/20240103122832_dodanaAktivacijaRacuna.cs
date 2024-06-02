using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodanaAktivacijaRacuna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "KorisnickiRacun",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RacunAktivacija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickiRacunID = table.Column<int>(type: "int", nullable: false),
                    ActivateKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacunAktivacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RacunAktivacija_KorisnickiRacun_KorisnickiRacunID",
                        column: x => x.KorisnickiRacunID,
                        principalTable: "KorisnickiRacun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RacunAktivacija_KorisnickiRacunID",
                table: "RacunAktivacija",
                column: "KorisnickiRacunID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RacunAktivacija");

            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "KorisnickiRacun");
        }
    }
}
