using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updated_ScheduleDetail_24032319350090 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "AppSalaries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TitleId",
                table: "AppSalaries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSalaries_DepartmentId",
                table: "AppSalaries",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalaries_TitleId",
                table: "AppSalaries",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSalaries_AppDepartments_DepartmentId",
                table: "AppSalaries",
                column: "DepartmentId",
                principalTable: "AppDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSalaries_AppTitles_TitleId",
                table: "AppSalaries",
                column: "TitleId",
                principalTable: "AppTitles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSalaries_AppDepartments_DepartmentId",
                table: "AppSalaries");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSalaries_AppTitles_TitleId",
                table: "AppSalaries");

            migrationBuilder.DropIndex(
                name: "IX_AppSalaries_DepartmentId",
                table: "AppSalaries");

            migrationBuilder.DropIndex(
                name: "IX_AppSalaries_TitleId",
                table: "AppSalaries");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AppSalaries");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "AppSalaries");
        }
    }
}
