using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update_Schedule_240404004040404 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StaffId",
                table: "AppSchedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppSchedules_StaffId",
                table: "AppSchedules",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSchedules_AppStaffs_StaffId",
                table: "AppSchedules",
                column: "StaffId",
                principalTable: "AppStaffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSchedules_AppStaffs_StaffId",
                table: "AppSchedules");

            migrationBuilder.DropIndex(
                name: "IX_AppSchedules_StaffId",
                table: "AppSchedules");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "AppSchedules");
        }
    }
}
