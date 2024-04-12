using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Added_Staff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppStaffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppStaffs_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppStaffs_AppTitles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "AppTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppStaffTimesheet",
                columns: table => new
                {
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimesheetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStaffTimesheet", x => new { x.StaffId, x.TimesheetId });
                    table.ForeignKey(
                        name: "FK_AppStaffTimesheet_AppStaffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AppStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppStaffTimesheet_AppTimesheets_TimesheetId",
                        column: x => x.TimesheetId,
                        principalTable: "AppTimesheets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppStaffs_DepartmentId",
                table: "AppStaffs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStaffs_TitleId",
                table: "AppStaffs",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStaffTimesheet_StaffId_TimesheetId",
                table: "AppStaffTimesheet",
                columns: new[] { "StaffId", "TimesheetId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppStaffTimesheet_TimesheetId",
                table: "AppStaffTimesheet",
                column: "TimesheetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppStaffTimesheet");

            migrationBuilder.DropTable(
                name: "AppStaffs");
        }
    }
}
