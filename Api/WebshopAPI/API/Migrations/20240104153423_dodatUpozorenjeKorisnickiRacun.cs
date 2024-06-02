using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodatUpozorenjeKorisnickiRacun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpozorenjeKorisnickiRacun",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickiRacunID = table.Column<int>(type: "int", nullable: false),
                    TipProblema = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpozorenjeKorisnickiRacun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpozorenjeKorisnickiRacun_KorisnickiRacun_KorisnickiRacunID",
                        column: x => x.KorisnickiRacunID,
                        principalTable: "KorisnickiRacun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpozorenjeKorisnickiRacun_KorisnickiRacunID",
                table: "UpozorenjeKorisnickiRacun",
                column: "KorisnickiRacunID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpozorenjeKorisnickiRacun");
        }
    }
}
