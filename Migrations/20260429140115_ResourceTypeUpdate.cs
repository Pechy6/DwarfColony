using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwarfColony.Migrations
{
    /// <inheritdoc />
    public partial class ResourceTypeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AreaResources_ResourceType_ResourceTypeId",
                table: "AreaResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceType",
                table: "ResourceType");

            migrationBuilder.RenameTable(
                name: "ResourceType",
                newName: "ResourceTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceTypes",
                table: "ResourceTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AreaResources_ResourceTypes_ResourceTypeId",
                table: "AreaResources",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AreaResources_ResourceTypes_ResourceTypeId",
                table: "AreaResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceTypes",
                table: "ResourceTypes");

            migrationBuilder.RenameTable(
                name: "ResourceTypes",
                newName: "ResourceType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceType",
                table: "ResourceType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AreaResources_ResourceType_ResourceTypeId",
                table: "AreaResources",
                column: "ResourceTypeId",
                principalTable: "ResourceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
