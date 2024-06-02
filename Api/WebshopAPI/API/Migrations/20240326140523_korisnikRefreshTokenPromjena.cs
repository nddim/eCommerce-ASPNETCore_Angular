using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class korisnikRefreshTokenPromjena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers",
                newName: "TokenExpires");

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ded8506-4ae7-4245-8300-82778a785ad2", "3", "Posjetilac", "Posjetilac" },
                    { "3e749de3-28f1-4f1b-890c-d06c1458cc84", "4", "Superadmin", "Superadmin" },
                    { "614ddae8-0c88-4668-8093-d274febfac2b", "1", "Admin", "Admin" },
                    { "86b9f5f9-1706-4e1a-8207-4fa2a01126bb", "2", "Kupac", "Kupac" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ded8506-4ae7-4245-8300-82778a785ad2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e749de3-28f1-4f1b-890c-d06c1458cc84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "614ddae8-0c88-4668-8093-d274febfac2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86b9f5f9-1706-4e1a-8207-4fa2a01126bb");

            migrationBuilder.DropColumn(
                name: "TokenCreated",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TokenExpires",
                table: "AspNetUsers",
                newName: "RefreshTokenExpiry");

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
    }
}
