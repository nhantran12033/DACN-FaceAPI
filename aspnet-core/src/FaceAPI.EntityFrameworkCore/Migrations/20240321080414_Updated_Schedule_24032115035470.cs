using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Schedule_24032115035470 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppScheduleScheduleDetail",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppScheduleScheduleDetail", x => new { x.ScheduleId, x.ScheduleDetailId });
                    table.ForeignKey(
                        name: "FK_AppScheduleScheduleDetail_AppScheduleDetails_ScheduleDetailId",
                        column: x => x.ScheduleDetailId,
                        principalTable: "AppScheduleDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppScheduleScheduleDetail_AppSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "AppSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleScheduleDetail_ScheduleDetailId",
                table: "AppScheduleScheduleDetail",
                column: "ScheduleDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleScheduleDetail_ScheduleId_ScheduleDetailId",
                table: "AppScheduleScheduleDetail",
                columns: new[] { "ScheduleId", "ScheduleDetailId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppScheduleScheduleDetail");
        }
    }
}
