using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jared.Infrastructure.Migrations
{
    public partial class JAR12UpdateEpic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Epic",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Epic",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Epic",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Epic",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Epic",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Epic",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Epic");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Epic");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Epic");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Epic");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Epic");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Epic",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
