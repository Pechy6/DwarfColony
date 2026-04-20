using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwarfColony.Migrations
{
    /// <inheritdoc />
    public partial class DwarfStateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Dwarves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Dwarves");
        }
    }
}
