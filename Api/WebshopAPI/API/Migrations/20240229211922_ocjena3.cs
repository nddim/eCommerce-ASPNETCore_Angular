using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ocjena3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KupacId",
                table: "Ocjena",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_KupacId",
                table: "Ocjena",
                column: "KupacId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Kupac_KupacId",
                table: "Ocjena",
                column: "KupacId",
                principalTable: "Kupac",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Kupac_KupacId",
                table: "Ocjena");

            migrationBuilder.DropIndex(
                name: "IX_Ocjena_KupacId",
                table: "Ocjena");

            migrationBuilder.DropColumn(
                name: "KupacId",
                table: "Ocjena");
        }
    }
}
