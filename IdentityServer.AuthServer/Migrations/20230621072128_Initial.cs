using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.AuthServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CustomUsers",
                columns: new[] { "Id", "City", "Email", "Password", "UserName" },
                values: new object[] { 1, "Istanbul", "ehlkara@hotmail.com", "password", "ehlkara" });

            migrationBuilder.InsertData(
                table: "CustomUsers",
                columns: new[] { "Id", "City", "Email", "Password", "UserName" },
                values: new object[] { 2, "Sivas", "ahmet@hotmail.com", "password", "ahmet16" });

            migrationBuilder.InsertData(
                table: "CustomUsers",
                columns: new[] { "Id", "City", "Email", "Password", "UserName" },
                values: new object[] { 3, "Balıkesir", "mehmet@hotmail.com", "password", "mehmet07" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomUsers");
        }
    }
}
