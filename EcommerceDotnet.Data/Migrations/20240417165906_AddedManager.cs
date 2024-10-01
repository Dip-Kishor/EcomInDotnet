using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceDotnet.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "3CBE903F356444F9822CDD1B7EEA462E4536DE591535EDA8DFE1685812A2517B");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 2, "37F9097993BD1296DB0D772D7333F080BCB3EF09EE5A1D8EF5BC16777734B65F", "manager@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918");
        }
    }
}
