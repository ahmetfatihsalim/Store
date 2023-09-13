using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaleStore.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Products",
                newName: "Id");
        }
    }
}
