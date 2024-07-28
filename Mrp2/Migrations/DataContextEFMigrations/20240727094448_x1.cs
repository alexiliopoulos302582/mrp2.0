using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mrp2.Migrations.DataContextEFMigrations
{
    /// <inheritdoc />
    public partial class x1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RawMaterialIds",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RawMaterialIds",
                table: "Order");
        }
    }
}
