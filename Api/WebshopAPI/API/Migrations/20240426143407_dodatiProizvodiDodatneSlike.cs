using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodatiProizvodiDodatneSlike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProizvodSlika_ProizvodId",
                table: "ProizvodSlika",
                column: "ProizvodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProizvodSlika_Proizvod_ProizvodId",
                table: "ProizvodSlika",
                column: "ProizvodId",
                principalTable: "Proizvod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProizvodSlika_Proizvod_ProizvodId",
                table: "ProizvodSlika");

            migrationBuilder.DropIndex(
                name: "IX_ProizvodSlika_ProizvodId",
                table: "ProizvodSlika");
        }
    }
}
