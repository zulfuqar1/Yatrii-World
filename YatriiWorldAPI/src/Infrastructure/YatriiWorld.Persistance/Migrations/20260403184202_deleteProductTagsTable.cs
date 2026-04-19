using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YatriiWorld.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class deleteProductTagsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductTags_ProductTags_ProductTagsId",
                table: "ProductProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags");

            migrationBuilder.RenameTable(
                name: "ProductTags",
                newName: "ProductTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductTags_ProductTag_ProductTagsId",
                table: "ProductProductTags",
                column: "ProductTagsId",
                principalTable: "ProductTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductTags_ProductTag_ProductTagsId",
                table: "ProductProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag");

            migrationBuilder.RenameTable(
                name: "ProductTag",
                newName: "ProductTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductTags_ProductTags_ProductTagsId",
                table: "ProductProductTags",
                column: "ProductTagsId",
                principalTable: "ProductTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
