using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class promjeneKorisnikKupac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExceptionLogovi_KorisnickiRacun_KorisnikID",
                table: "ExceptionLogovi");

            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_Kupac_KupacId",
                table: "Korpa");

            migrationBuilder.DropForeignKey(
                name: "FK_LogKretanjePoSistemu_KorisnickiRacun_KorisnikID",
                table: "LogKretanjePoSistemu");

            migrationBuilder.DropForeignKey(
                name: "FK_LogOdjava_KorisnickiRacun_KorisnikID",
                table: "LogOdjava");

            migrationBuilder.DropForeignKey(
                name: "FK_LogPrijava_KorisnickiRacun_KorisnikID",
                table: "LogPrijava");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Kupac_KupacId",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Kupac_KupacId",
                table: "Ocjena");

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

            migrationBuilder.AlterColumn<string>(
                name: "KupacId",
                table: "Ocjena",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "KupacId",
                table: "Narudzba",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikID",
                table: "LogPrijava",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikID",
                table: "LogOdjava",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikID",
                table: "LogKretanjePoSistemu",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "KupacId",
                table: "Korpa",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikID",
                table: "ExceptionLogovi",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumKreiranja",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumModifikovanja",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ExceptionLogovi_AspNetUsers_KorisnikID",
                table: "ExceptionLogovi",
                column: "KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_AspNetUsers_KupacId",
                table: "Korpa",
                column: "KupacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogKretanjePoSistemu_AspNetUsers_KorisnikID",
                table: "LogKretanjePoSistemu",
                column: "KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogOdjava_AspNetUsers_KorisnikID",
                table: "LogOdjava",
                column: "KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogPrijava_AspNetUsers_KorisnikID",
                table: "LogPrijava",
                column: "KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_AspNetUsers_KupacId",
                table: "Narudzba",
                column: "KupacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_AspNetUsers_KupacId",
                table: "Ocjena",
                column: "KupacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExceptionLogovi_AspNetUsers_KorisnikID",
                table: "ExceptionLogovi");

            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_AspNetUsers_KupacId",
                table: "Korpa");

            migrationBuilder.DropForeignKey(
                name: "FK_LogKretanjePoSistemu_AspNetUsers_KorisnikID",
                table: "LogKretanjePoSistemu");

            migrationBuilder.DropForeignKey(
                name: "FK_LogOdjava_AspNetUsers_KorisnikID",
                table: "LogOdjava");

            migrationBuilder.DropForeignKey(
                name: "FK_LogPrijava_AspNetUsers_KorisnikID",
                table: "LogPrijava");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_AspNetUsers_KupacId",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_AspNetUsers_KupacId",
                table: "Ocjena");

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

            migrationBuilder.DropColumn(
                name: "DatumKreiranja",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DatumModifikovanja",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "KupacId",
                table: "Ocjena",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "KupacId",
                table: "Narudzba",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikID",
                table: "LogPrijava",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikID",
                table: "LogOdjava",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikID",
                table: "LogKretanjePoSistemu",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KupacId",
                table: "Korpa",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikID",
                table: "ExceptionLogovi",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ExceptionLogovi_KorisnickiRacun_KorisnikID",
                table: "ExceptionLogovi",
                column: "KorisnikID",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_Kupac_KupacId",
                table: "Korpa",
                column: "KupacId",
                principalTable: "Kupac",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogKretanjePoSistemu_KorisnickiRacun_KorisnikID",
                table: "LogKretanjePoSistemu",
                column: "KorisnikID",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogOdjava_KorisnickiRacun_KorisnikID",
                table: "LogOdjava",
                column: "KorisnikID",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogPrijava_KorisnickiRacun_KorisnikID",
                table: "LogPrijava",
                column: "KorisnikID",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Kupac_KupacId",
                table: "Narudzba",
                column: "KupacId",
                principalTable: "Kupac",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Kupac_KupacId",
                table: "Ocjena",
                column: "KupacId",
                principalTable: "Kupac",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
