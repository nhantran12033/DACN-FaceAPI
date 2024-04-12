using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Staff_24032211451281 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Debt",
                table: "AppStaffs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Debt",
                table: "AppStaffs");
        }
    }
}
