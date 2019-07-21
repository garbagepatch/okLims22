using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class quest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "Request",
                nullable: false,
                defaultValue: 0);

     
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "Request");

          
        }
    }
}
