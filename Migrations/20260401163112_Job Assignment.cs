using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwarfColony.Migrations
{
    /// <inheritdoc />
    public partial class JobAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Job",
                table: "Dwarves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Job",
                table: "Dwarves");
        }
    }
}
