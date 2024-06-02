using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class iniciranProjekatSaKlasama : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artikl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brend",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlavnaKategorija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlavnaKategorija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlavnaKategorijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kategorija_GlavnaKategorija_GlavnaKategorijaID",
                        column: x => x.GlavnaKategorijaID,
                        principalTable: "GlavnaKategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Potkategorija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategorijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potkategorija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Potkategorija_Kategorija_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PocetnaKolicina = table.Column<int>(type: "int", nullable: false),
                    PocetnaCijena = table.Column<float>(type: "real", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojKlikova = table.Column<int>(type: "int", nullable: false),
                    BrendID = table.Column<int>(type: "int", nullable: false),
                    PotkategorijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proizvod_Brend_BrendID",
                        column: x => x.BrendID,
                        principalTable: "Brend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvod_Potkategorija_PotkategorijaID",
                        column: x => x.PotkategorijaID,
                        principalTable: "Potkategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kategorija_GlavnaKategorijaID",
                table: "Kategorija",
                column: "GlavnaKategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Potkategorija_KategorijaID",
                table: "Potkategorija",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_BrendID",
                table: "Proizvod",
                column: "BrendID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_PotkategorijaID",
                table: "Proizvod",
                column: "PotkategorijaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artikl");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "Brend");

            migrationBuilder.DropTable(
                name: "Potkategorija");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "GlavnaKategorija");
        }
    }
}
