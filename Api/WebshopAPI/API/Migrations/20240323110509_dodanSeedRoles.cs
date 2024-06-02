using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodanSeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "614e0e15-d3f5-4dc6-a7fe-98287fb66b70", "3", "Posjetilac", "Posjetilac" },
                    { "92c555da-9af5-40a2-9eab-ed7243a91137", "1", "Admin", "Admin" },
                    { "df144892-5227-464e-923b-3e3121b7d3bf", "2", "Kupac", "Kupac" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "614e0e15-d3f5-4dc6-a7fe-98287fb66b70");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92c555da-9af5-40a2-9eab-ed7243a91137");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df144892-5227-464e-923b-3e3121b7d3bf");
        }
    }
}
