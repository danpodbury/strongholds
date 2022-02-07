using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrongholdsAPI.Migrations
{
    public partial class uncycling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Robots_Stations_StationID",
                table: "Robots");

            migrationBuilder.DropIndex(
                name: "IX_Robots_StationID",
                table: "Robots");

            migrationBuilder.DropColumn(
                name: "StationID",
                table: "Robots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StationID",
                table: "Robots",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Robots_StationID",
                table: "Robots",
                column: "StationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Robots_Stations_StationID",
                table: "Robots",
                column: "StationID",
                principalTable: "Stations",
                principalColumn: "StationID");
        }
    }
}
