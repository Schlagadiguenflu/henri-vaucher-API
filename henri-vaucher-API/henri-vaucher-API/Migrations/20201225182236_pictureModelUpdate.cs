using Microsoft.EntityFrameworkCore.Migrations;

namespace henri_vaucher_API.Migrations
{
    public partial class pictureModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pictures_Number",
                table: "Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Pictures",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Pictures",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_Number",
                table: "Pictures",
                column: "Number",
                unique: true);
        }
    }
}
