using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Migrations
{
    public partial class AddFKNoteToNoteType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteTypeId",
                table: "Notes",
                column: "NoteTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteTypes_NoteTypeId",
                table: "Notes",
                column: "NoteTypeId",
                principalTable: "NoteTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteTypes_NoteTypeId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_NoteTypeId",
                table: "Notes");
        }
    }
}
