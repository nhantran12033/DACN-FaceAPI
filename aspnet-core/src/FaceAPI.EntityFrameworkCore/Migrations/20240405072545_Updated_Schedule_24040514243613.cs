using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Schedule_24040514243613 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSchedules_AppDepartments_DepartmentId",
                table: "AppSchedules");

            migrationBuilder.DropIndex(
                name: "IX_AppSchedules_DepartmentId",
                table: "AppSchedules");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AppSchedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "AppSchedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSchedules_DepartmentId",
                table: "AppSchedules",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSchedules_AppDepartments_DepartmentId",
                table: "AppSchedules",
                column: "DepartmentId",
                principalTable: "AppDepartments",
                principalColumn: "Id");
        }
    }
}
