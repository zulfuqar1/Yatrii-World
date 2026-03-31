using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YatriiWorld.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class TourRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Tours",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Tours");
        }
    }
}
