using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dodatRefreshTokenKorisnik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "269aafa7-68bf-4124-a120-28bf59872f9a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cafb669-ffd5-4ec6-84d2-8258d376b39f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4d740cc-818e-4fc5-9655-acc1c2cf3637");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e93cd39c-851d-4ce2-81c3-d65b806e0426");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2819b602-96ae-4843-9480-fa2eca6c9f38", "3", "Posjetilac", "Posjetilac" },
                    { "4844bb1c-78ed-4141-b54a-b394f854760b", "1", "Admin", "Admin" },
                    { "4e60ae8c-7d97-4ed3-b500-514b979a024b", "4", "Superadmin", "Superadmin" },
                    { "cb400403-0e5d-4ccf-aa8d-5641c28af484", "2", "Kupac", "Kupac" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2819b602-96ae-4843-9480-fa2eca6c9f38");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4844bb1c-78ed-4141-b54a-b394f854760b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e60ae8c-7d97-4ed3-b500-514b979a024b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb400403-0e5d-4ccf-aa8d-5641c28af484");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "269aafa7-68bf-4124-a120-28bf59872f9a", "1", "Admin", "Admin" },
                    { "7cafb669-ffd5-4ec6-84d2-8258d376b39f", "3", "Posjetilac", "Posjetilac" },
                    { "e4d740cc-818e-4fc5-9655-acc1c2cf3637", "2", "Kupac", "Kupac" },
                    { "e93cd39c-851d-4ce2-81c3-d65b806e0426", "4", "Superadmin", "Superadmin" }
                });
        }
    }
}
