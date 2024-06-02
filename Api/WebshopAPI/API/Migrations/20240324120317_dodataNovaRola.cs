using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodataNovaRola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39eb4d94-2bf0-49a5-a7aa-d36fdd02a59f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54105cbb-ef34-4fa2-bc78-a0d9c2446e3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac0266d3-7c13-4640-818d-713781423d8a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a1bd8aa-03a7-4c25-be17-08547a7ec930", "2", "Kupac", "Kupac" },
                    { "5d48faad-0091-41d7-b88a-c04c677e207a", "4", "Superadmin", "Superadmin" },
                    { "67b9a30e-6114-4df2-ab37-5fdb84bee9d0", "3", "Posjetilac", "Posjetilac" },
                    { "9af8f43a-2036-40a3-9fa9-4b9c00179591", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a1bd8aa-03a7-4c25-be17-08547a7ec930");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d48faad-0091-41d7-b88a-c04c677e207a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67b9a30e-6114-4df2-ab37-5fdb84bee9d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9af8f43a-2036-40a3-9fa9-4b9c00179591");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39eb4d94-2bf0-49a5-a7aa-d36fdd02a59f", "3", "Posjetilac", "Posjetilac" },
                    { "54105cbb-ef34-4fa2-bc78-a0d9c2446e3a", "1", "Admin", "Admin" },
                    { "ac0266d3-7c13-4640-818d-713781423d8a", "2", "Kupac", "Kupac" }
                });
        }
    }
}
