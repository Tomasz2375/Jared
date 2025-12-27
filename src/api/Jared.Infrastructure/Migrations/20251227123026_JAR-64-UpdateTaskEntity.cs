using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JAR64UpdateTaskEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstimatedTimeMinutes",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalWorkTimeMinutes",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                UPDATE Task
                SET EstimatedTimeMinutes = DATEDIFF(MINUTE, 0, EstimatedTime),
                    TotalWorkTimeMinutes = DATEDIFF(MINUTE, 0, TotalWorkTime)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedTimeMinutes",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "TotalWorkTimeMinutes",
                table: "Task");
        }
    }
}
