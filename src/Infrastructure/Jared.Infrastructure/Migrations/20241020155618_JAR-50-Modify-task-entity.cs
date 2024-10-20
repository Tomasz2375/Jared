using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jared.Infrastructure.Migrations
{
    public partial class JAR50Modifytaskentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedToId",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_AssignedToId",
                table: "Task",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_CreatedById",
                table: "Task",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_AssignedToId",
                table: "Task",
                column: "AssignedToId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_CreatedById",
                table: "Task",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_AssignedToId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_CreatedById",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_AssignedToId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_CreatedById",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Task");
        }
    }
}
