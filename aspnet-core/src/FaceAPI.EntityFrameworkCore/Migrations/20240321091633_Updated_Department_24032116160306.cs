using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Department_24032116160306 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDepartmentTitle",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDepartmentTitle", x => new { x.DepartmentId, x.TitleId });
                    table.ForeignKey(
                        name: "FK_AppDepartmentTitle_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppDepartmentTitle_AppTitles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "AppTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDepartmentTitle_DepartmentId_TitleId",
                table: "AppDepartmentTitle",
                columns: new[] { "DepartmentId", "TitleId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppDepartmentTitle_TitleId",
                table: "AppDepartmentTitle",
                column: "TitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDepartmentTitle");
        }
    }
}
