using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update_ScheduleDetail_240404004040404 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppScheduleDetailScheduleFormat",
                columns: table => new
                {
                    ScheduleDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleFormatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppScheduleDetailScheduleFormat", x => new { x.ScheduleDetailId, x.ScheduleFormatId });
                    table.ForeignKey(
                        name: "FK_AppScheduleDetailScheduleFormat_AppScheduleDetails_ScheduleDetailId",
                        column: x => x.ScheduleDetailId,
                        principalTable: "AppScheduleDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppScheduleDetailScheduleFormat_AppScheduleFormats_ScheduleFormatId",
                        column: x => x.ScheduleFormatId,
                        principalTable: "AppScheduleFormats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleDetailScheduleFormat_ScheduleDetailId_ScheduleFormatId",
                table: "AppScheduleDetailScheduleFormat",
                columns: new[] { "ScheduleDetailId", "ScheduleFormatId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleDetailScheduleFormat_ScheduleFormatId",
                table: "AppScheduleDetailScheduleFormat",
                column: "ScheduleFormatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppScheduleDetailScheduleFormat");
        }
    }
}
