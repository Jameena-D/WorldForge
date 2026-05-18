using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSectionsAndBlocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorldBlock_WorldSection_WorldSectionId",
                table: "WorldBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_WorldSection_Worlds_WorldId",
                table: "WorldSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorldSection",
                table: "WorldSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorldBlock",
                table: "WorldBlock");

            migrationBuilder.RenameTable(
                name: "WorldSection",
                newName: "WorldSections");

            migrationBuilder.RenameTable(
                name: "WorldBlock",
                newName: "WorldBlocks");

            migrationBuilder.RenameIndex(
                name: "IX_WorldSection_WorldId",
                table: "WorldSections",
                newName: "IX_WorldSections_WorldId");

            migrationBuilder.RenameIndex(
                name: "IX_WorldBlock_WorldSectionId",
                table: "WorldBlocks",
                newName: "IX_WorldBlocks_WorldSectionId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WorldSections",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorldBlocks",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "WorldBlocks",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorldSections",
                table: "WorldSections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorldBlocks",
                table: "WorldBlocks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorldBlocks_WorldSections_WorldSectionId",
                table: "WorldBlocks",
                column: "WorldSectionId",
                principalTable: "WorldSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorldSections_Worlds_WorldId",
                table: "WorldSections",
                column: "WorldId",
                principalTable: "Worlds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorldBlocks_WorldSections_WorldSectionId",
                table: "WorldBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_WorldSections_Worlds_WorldId",
                table: "WorldSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorldSections",
                table: "WorldSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorldBlocks",
                table: "WorldBlocks");

            migrationBuilder.RenameTable(
                name: "WorldSections",
                newName: "WorldSection");

            migrationBuilder.RenameTable(
                name: "WorldBlocks",
                newName: "WorldBlock");

            migrationBuilder.RenameIndex(
                name: "IX_WorldSections_WorldId",
                table: "WorldSection",
                newName: "IX_WorldSection_WorldId");

            migrationBuilder.RenameIndex(
                name: "IX_WorldBlocks_WorldSectionId",
                table: "WorldBlock",
                newName: "IX_WorldBlock_WorldSectionId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WorldSection",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorldBlock",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "WorldBlock",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorldSection",
                table: "WorldSection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorldBlock",
                table: "WorldBlock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorldBlock_WorldSection_WorldSectionId",
                table: "WorldBlock",
                column: "WorldSectionId",
                principalTable: "WorldSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorldSection_Worlds_WorldId",
                table: "WorldSection",
                column: "WorldId",
                principalTable: "Worlds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
