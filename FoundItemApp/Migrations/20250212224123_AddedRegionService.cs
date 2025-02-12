using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace FoundItemApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedRegionService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Geometry>(
                name: "Borders",
                table: "Regions",
                type: "geography",
                nullable: false,
                oldClrType: typeof(Geometry),
                oldType: "geography",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Geometry>(
                name: "Borders",
                table: "Regions",
                type: "geography",
                nullable: true,
                oldClrType: typeof(Geometry),
                oldType: "geography");
        }
    }
}
