using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SaleStore.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ISBN = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    ListPrice = table.Column<double>(type: "double precision", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Price50 = table.Column<double>(type: "double precision", nullable: false),
                    Price100 = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "John Smith", "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", "ABCDEF1234", 100.0, 80.0, 60.0, 65.0, "Lorem" },
                    { 2, "Sarah Johnson", "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod.", "ZXCVB0987Y", 120.0, 90.0, 70.0, 75.0, "Ipsum" },
                    { 3, "David Brown", "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor.", "QWERT5678P", 80.0, 60.0, 52.0, 55.0, "Dolor" },
                    { 4, "Emily Davis", "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed.", "ASDFG1234H", 140.0, 110.0, 95.0, 100.0, "Sit" },
                    { 5, "Michael Wilson", "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt.", "JKLPO6789I", 70.0, 55.0, 48.0, 50.0, "Amet" },
                    { 6, "Jessica Thompson", "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "MNBVC4321U", 130.0, 100.0, 85.0, 90.0, "Consectetur" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);
        }
    }
}
