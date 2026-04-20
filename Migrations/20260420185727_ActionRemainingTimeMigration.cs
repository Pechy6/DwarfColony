using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwarfColony.Migrations
{
    /// <inheritdoc />
    public partial class ActionRemainingTimeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionRemainingTime",
                table: "Dwarves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionRemainingTime",
                table: "Dwarves");
        }
    }
}
