using EcommerceDotnet.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceDotnet.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedusermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
        protected void Seed(EcommerceContext context)
        {
            context.Users.Add(
                new UserModel() { Id = 10, Username = "Admin@gmail.com", Password="admin" }
                );
        }
    }
}
