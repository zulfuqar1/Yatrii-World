using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YatriiWorld.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxonomyEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductTags",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductTagsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductTags", x => new { x.ProductId, x.ProductTagsId });
                    table.ForeignKey(
                        name: "FK_ProductProductTags_ProductTags_ProductTagsId",
                        column: x => x.ProductTagsId,
                        principalTable: "ProductTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductTags_ProductTagsId",
                table: "ProductProductTags",
                column: "ProductTagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropTable(
          //      name: "ProductProductTags");

         //   migrationBuilder.DropTable(
           //     name: "ProductTags");
        }
    }
}
