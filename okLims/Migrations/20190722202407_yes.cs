using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class yes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "RequestLine",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "RequestLine");
        }
    }
}
