using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwarfColony.Migrations
{
    /// <inheritdoc />
    public partial class AddTravelProperitiesToDwarf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetAreaId",
                table: "Dwarves",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TravelRemainingTicks",
                table: "Dwarves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetAreaId",
                table: "Dwarves");

            migrationBuilder.DropColumn(
                name: "TravelRemainingTicks",
                table: "Dwarves");
        }
    }
}
