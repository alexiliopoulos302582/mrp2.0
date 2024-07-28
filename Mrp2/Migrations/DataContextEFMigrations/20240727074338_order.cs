using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mrp2.Migrations.DataContextEFMigrations
{
    /// <inheritdoc />
    public partial class order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Po = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RawMaterialCost = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RecipeCost = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OrderTotalCost = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaterialsReserved = table.Column<bool>(type: "bit", nullable: false),
                    OrderCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
