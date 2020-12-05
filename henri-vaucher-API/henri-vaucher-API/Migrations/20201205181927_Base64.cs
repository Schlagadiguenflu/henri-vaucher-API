using Microsoft.EntityFrameworkCore.Migrations;

namespace henri_vaucher_API.Migrations
{
    public partial class Base64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Pictures",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Pictures");
        }
    }
}
