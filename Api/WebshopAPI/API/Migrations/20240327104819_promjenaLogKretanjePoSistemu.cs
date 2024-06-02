using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class promjenaLogKretanjePoSistemu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogKretanjePoSistemu_AspNetUsers_KorisnikID",
                table: "LogKretanjePoSistemu");

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

            migrationBuilder.RenameColumn(
                name: "KorisnikID",
                table: "LogKretanjePoSistemu",
                newName: "KorisnikId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "LogKretanjePoSistemu",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_LogKretanjePoSistemu_KorisnikID",
                table: "LogKretanjePoSistemu",
                newName: "IX_LogKretanjePoSistemu_KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogKretanjePoSistemu_AspNetUsers_KorisnikId",
                table: "LogKretanjePoSistemu",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogKretanjePoSistemu_AspNetUsers_KorisnikId",
                table: "LogKretanjePoSistemu");

            migrationBuilder.RenameColumn(
                name: "KorisnikId",
                table: "LogKretanjePoSistemu",
                newName: "KorisnikID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LogKretanjePoSistemu",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_LogKretanjePoSistemu_KorisnikId",
                table: "LogKretanjePoSistemu",
                newName: "IX_LogKretanjePoSistemu_KorisnikID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LogKretanjePoSistemu_AspNetUsers_KorisnikID",
                table: "LogKretanjePoSistemu",
                column: "KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
