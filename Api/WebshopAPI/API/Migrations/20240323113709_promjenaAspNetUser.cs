using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class promjenaAspNetUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SaljiNovosti",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SaljiNovosti",
                table: "AspNetUsers");

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
    }
}
