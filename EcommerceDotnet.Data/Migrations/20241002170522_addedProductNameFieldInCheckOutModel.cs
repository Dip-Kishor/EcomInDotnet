using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceDotnet.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedProductNameFieldInCheckOutModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CheckoutItems",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CheckoutItems");
        }
    }
}
