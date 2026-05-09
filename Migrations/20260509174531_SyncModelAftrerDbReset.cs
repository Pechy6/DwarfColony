using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwarfColony.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelAftrerDbReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dwarves_Areas_CurrentAreaId",
                table: "Dwarves");

            migrationBuilder.CreateIndex(
                name: "IX_Dwarves_TargetAreaId",
                table: "Dwarves",
                column: "TargetAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dwarves_Areas_CurrentAreaId",
                table: "Dwarves",
                column: "CurrentAreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dwarves_Areas_TargetAreaId",
                table: "Dwarves",
                column: "TargetAreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dwarves_Areas_CurrentAreaId",
                table: "Dwarves");

            migrationBuilder.DropForeignKey(
                name: "FK_Dwarves_Areas_TargetAreaId",
                table: "Dwarves");

            migrationBuilder.DropIndex(
                name: "IX_Dwarves_TargetAreaId",
                table: "Dwarves");

            migrationBuilder.AddForeignKey(
                name: "FK_Dwarves_Areas_CurrentAreaId",
                table: "Dwarves",
                column: "CurrentAreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
