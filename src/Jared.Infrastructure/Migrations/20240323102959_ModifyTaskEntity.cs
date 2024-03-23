using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jared.Infrastructure.Migrations
{
    public partial class ModifyTaskEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Task",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "PriorityId",
                table: "Task",
                newName: "Priority");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Task",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Task",
                newName: "PriorityId");
        }
    }
}
