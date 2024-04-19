using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Timesheets_423232304040040040404 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoursWork",
                table: "AppTimesheets");

            migrationBuilder.DropColumn(
                name: "TimeIn",
                table: "AppTimesheets");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "AppTimesheets");

   

            migrationBuilder.RenameColumn(
                name: "TimeOut",
                table: "AppTimesheets",
                newName: "Time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "AppTimesheets",
                newName: "TimeOut");

            migrationBuilder.AddColumn<int>(
                name: "HoursWork",
                table: "AppTimesheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeIn",
                table: "AppTimesheets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "AppTimesheets",
                type: "bit",
                nullable: false,
                defaultValue: false);

        }
    }
}
