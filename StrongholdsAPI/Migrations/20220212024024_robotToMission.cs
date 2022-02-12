using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrongholdsAPI.Migrations
{
    public partial class robotToMission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Missions_RobotID",
                table: "Missions");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_RobotID",
                table: "Missions",
                column: "RobotID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Missions_RobotID",
                table: "Missions");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_RobotID",
                table: "Missions",
                column: "RobotID");
        }
    }
}
