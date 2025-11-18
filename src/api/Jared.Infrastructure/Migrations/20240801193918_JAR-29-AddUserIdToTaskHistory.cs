using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jared.Infrastructure.Migrations
{
    public partial class JAR29AddUserIdToTaskHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TaskHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_UserId",
                table: "TaskHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistory_User_UserId",
                table: "TaskHistory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistory_User_UserId",
                table: "TaskHistory");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistory_UserId",
                table: "TaskHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskHistory");
        }
    }
}
