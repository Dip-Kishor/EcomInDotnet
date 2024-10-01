using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommerceDotnet.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedEmailField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "3CBE903F356444F9822CDD1B7EEA462E4536DE591535EDA8DFE1685812A2517B", 0, "admin@gmail.com" },
                    { 2, "37F9097993BD1296DB0D772D7333F080BCB3EF09EE5A1D8EF5BC16777734B65F", 0, "manager@gmail.com" }
                });
        }
    }
}
