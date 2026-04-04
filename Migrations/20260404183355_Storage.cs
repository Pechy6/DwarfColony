using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwarfColony.Migrations
{
    /// <inheritdoc />
    public partial class Storage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawFood = table.Column<int>(type: "int", nullable: false),
                    Food = table.Column<int>(type: "int", nullable: false),
                    Water = table.Column<int>(type: "int", nullable: false),
                    Stone = table.Column<int>(type: "int", nullable: false),
                    IronCore = table.Column<int>(type: "int", nullable: false),
                    Coal = table.Column<int>(type: "int", nullable: false),
                    Wood = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Storages");
        }
    }
}
