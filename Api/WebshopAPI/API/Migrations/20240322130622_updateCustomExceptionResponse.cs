using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateCustomExceptionResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomResponse_KorisnickiRacun_KorisnikID",
                table: "CustomResponse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomResponse",
                table: "CustomResponse");

            migrationBuilder.RenameTable(
                name: "CustomResponse",
                newName: "ExceptionLogovi");

            migrationBuilder.RenameIndex(
                name: "IX_CustomResponse_KorisnikID",
                table: "ExceptionLogovi",
                newName: "IX_ExceptionLogovi_KorisnikID");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "ExceptionLogovi",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "ExceptionLogovi",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Metoda",
                table: "ExceptionLogovi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExceptionLogovi",
                table: "ExceptionLogovi",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExceptionLogovi_KorisnickiRacun_KorisnikID",
                table: "ExceptionLogovi",
                column: "KorisnikID",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExceptionLogovi_KorisnickiRacun_KorisnikID",
                table: "ExceptionLogovi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExceptionLogovi",
                table: "ExceptionLogovi");

            migrationBuilder.DropColumn(
                name: "Metoda",
                table: "ExceptionLogovi");

            migrationBuilder.RenameTable(
                name: "ExceptionLogovi",
                newName: "CustomResponse");

            migrationBuilder.RenameIndex(
                name: "IX_ExceptionLogovi_KorisnikID",
                table: "CustomResponse",
                newName: "IX_CustomResponse_KorisnikID");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "CustomResponse",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "CustomResponse",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomResponse",
                table: "CustomResponse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomResponse_KorisnickiRacun_KorisnikID",
                table: "CustomResponse",
                column: "KorisnikID",
                principalTable: "KorisnickiRacun",
                principalColumn: "Id");
        }
    }
}
