using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JAR55AddColorToProjectEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Project",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Project");
        }
    }
}
