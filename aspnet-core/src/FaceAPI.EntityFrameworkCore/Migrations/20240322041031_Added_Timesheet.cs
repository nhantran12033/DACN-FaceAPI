using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Added_Timesheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTimesheets",
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
                    TimeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoursWork = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ScheduleDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTimesheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTimesheets_AppScheduleDetails_ScheduleDetailId",
                        column: x => x.ScheduleDetailId,
                        principalTable: "AppScheduleDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppTimesheets_AppSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "AppSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTimesheets_ScheduleDetailId",
                table: "AppTimesheets",
                column: "ScheduleDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTimesheets_ScheduleId",
                table: "AppTimesheets",
                column: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTimesheets");
        }
    }
}
