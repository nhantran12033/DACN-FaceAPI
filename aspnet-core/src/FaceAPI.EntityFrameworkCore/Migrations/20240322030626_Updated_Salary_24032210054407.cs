using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Salary_24032210054407 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSalaryDepartment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSalaryDepartment",
                columns: table => new
                {
                    SalaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalaryDepartment", x => new { x.SalaryId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_AppSalaryDepartment_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalaryDepartment_AppSalaries_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "AppSalaries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSalaryDepartment_DepartmentId",
                table: "AppSalaryDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalaryDepartment_SalaryId_DepartmentId",
                table: "AppSalaryDepartment",
                columns: new[] { "SalaryId", "DepartmentId" });
        }
    }
}
