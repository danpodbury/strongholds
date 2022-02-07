using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrongholdsAPI.Migrations
{
    public partial class connections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stations_LoginID",
                table: "Stations",
                column: "LoginID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Logins_LoginID",
                table: "Stations",
                column: "LoginID",
                principalTable: "Logins",
                principalColumn: "LoginID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Logins_LoginID",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_Stations_LoginID",
                table: "Stations");
        }
    }
}
