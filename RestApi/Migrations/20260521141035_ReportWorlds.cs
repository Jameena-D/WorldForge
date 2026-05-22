using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace RestApi.Migrations
{
    /// <inheritdoc />
    public partial class ReportWorlds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportWorlds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    WorldId = table.Column<int>(type: "int", nullable: false),
                    ReporterUserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsHandled = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportWorlds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportWorlds_AspNetUsers_ReporterUserId",
                        column: x => x.ReporterUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportWorlds_Worlds_WorldId",
                        column: x => x.WorldId,
                        principalTable: "Worlds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ReportWorlds_ReporterUserId",
                table: "ReportWorlds",
                column: "ReporterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportWorlds_WorldId",
                table: "ReportWorlds",
                column: "WorldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportWorlds");
        }
    }
}
