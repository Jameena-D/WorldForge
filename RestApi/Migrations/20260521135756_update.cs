using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApi.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReporterUserId",
                table: "Reports",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReporterUserId",
                table: "Reports",
                column: "ReporterUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_ReporterUserId",
                table: "Reports",
                column: "ReporterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_ReporterUserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ReporterUserId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReporterUserId",
                table: "Reports");
        }
    }
}
