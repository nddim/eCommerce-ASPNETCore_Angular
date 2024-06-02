using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class racunAktivacijaPromjena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RacunAktivacija_KorisnickiRacun_KorisnickiRacunID",
                table: "RacunAktivacija");

            migrationBuilder.DropIndex(
                name: "IX_RacunAktivacija_KorisnickiRacunID",
                table: "RacunAktivacija");

            migrationBuilder.DropColumn(
                name: "KorisnickiRacunID",
                table: "RacunAktivacija");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumKreiranja",
                table: "RacunAktivacija",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumValidnost",
                table: "RacunAktivacija",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "KorisnikId",
                table: "RacunAktivacija",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RacunAktivacija_KorisnikId",
                table: "RacunAktivacija",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_RacunAktivacija_AspNetUsers_KorisnikId",
                table: "RacunAktivacija",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RacunAktivacija_AspNetUsers_KorisnikId",
                table: "RacunAktivacija");

            migrationBuilder.DropIndex(
                name: "IX_RacunAktivacija_KorisnikId",
                table: "RacunAktivacija");

            migrationBuilder.DropColumn(
                name: "DatumKreiranja",
                table: "RacunAktivacija");

            migrationBuilder.DropColumn(
                name: "DatumValidnost",
                table: "RacunAktivacija");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "RacunAktivacija");

            migrationBuilder.AddColumn<int>(
                name: "KorisnickiRacunID",
                table: "RacunAktivacija",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RacunAktivacija_KorisnickiRacunID",
                table: "RacunAktivacija",
                column: "KorisnickiRacunID");

            migrationBuilder.AddForeignKey(
                name: "FK_RacunAktivacija_KorisnickiRacun_KorisnickiRacunID",
                table: "RacunAktivacija",
                column: "KorisnickiRacunID",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
