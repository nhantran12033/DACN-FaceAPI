using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updated_TimeSheet_40404040040440 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleFormatId",
                table: "AppTimesheets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppTimesheets_ScheduleFormatId",
                table: "AppTimesheets",
                column: "ScheduleFormatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTimesheets_AppScheduleFormats_ScheduleFormatId",
                table: "AppTimesheets",
                column: "ScheduleFormatId",
                principalTable: "AppScheduleFormats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTimesheets_AppScheduleFormats_ScheduleFormatId",
                table: "AppTimesheets");

            migrationBuilder.DropIndex(
                name: "IX_AppTimesheets_ScheduleFormatId",
                table: "AppTimesheets");

            migrationBuilder.DropColumn(
                name: "ScheduleFormatId",
                table: "AppTimesheets");
        }
    }
}
