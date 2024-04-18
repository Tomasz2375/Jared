using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jared.Infrastructure.Migrations
{
    public partial class FixReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Epic_EpicId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Epic_EpicId",
                table: "Task",
                column: "EpicId",
                principalTable: "Epic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Epic_EpicId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Epic_EpicId",
                table: "Task",
                column: "EpicId",
                principalTable: "Epic",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
