using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mrp2.Migrations.DataContextEFMigrations
{
    /// <inheritdoc />
    public partial class orderstables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderRawMaterials",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRawMaterials", x => new { x.OrderId, x.RawMaterialId });
                    table.ForeignKey(
                        name: "FK_OrderRawMaterials_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRawMaterials_RawMaterial_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_RecipeId",
                table: "Order",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRawMaterials_RawMaterialId",
                table: "OrderRawMaterials",
                column: "RawMaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Recipe_RecipeId",
                table: "Order",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Recipe_RecipeId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "OrderRawMaterials");

            migrationBuilder.DropIndex(
                name: "IX_Order_RecipeId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Order");
        }
    }
}
