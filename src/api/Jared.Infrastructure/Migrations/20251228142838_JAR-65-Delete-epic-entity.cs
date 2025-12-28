using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JAR65Deleteepicentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Epic_EpicId",
                table: "Task");

            migrationBuilder.DropTable(
                name: "Epic");

            migrationBuilder.DropIndex(
                name: "IX_Task_EpicId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "EpicId",
                table: "Task");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EpicId",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Epic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Epic_Epic_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Epic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Epic_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_EpicId",
                table: "Task",
                column: "EpicId");

            migrationBuilder.CreateIndex(
                name: "IX_Epic_ParentId",
                table: "Epic",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Epic_ProjectId",
                table: "Epic",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Epic_EpicId",
                table: "Task",
                column: "EpicId",
                principalTable: "Epic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
