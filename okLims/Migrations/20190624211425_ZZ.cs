using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class ZZ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LaboratoryId",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MethodId",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MethodId",
                table: "MethodLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestLine_RequestId",
                table: "RequestLine",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_LaboratoryId",
                table: "Request",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_MethodId",
                table: "Request",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodLine_MethodId",
                table: "MethodLine",
                column: "MethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_MethodLine_Method_MethodId",
                table: "MethodLine",
                column: "MethodId",
                principalTable: "Method",
                principalColumn: "MethodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Laboratory_LaboratoryId",
                table: "Request",
                column: "LaboratoryId",
                principalTable: "Laboratory",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Method_MethodId",
                table: "Request",
                column: "MethodId",
                principalTable: "Method",
                principalColumn: "MethodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Request_RequestId",
                table: "RequestLine",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MethodLine_Method_MethodId",
                table: "MethodLine");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Laboratory_LaboratoryId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Method_MethodId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Request_RequestId",
                table: "RequestLine");

            migrationBuilder.DropIndex(
                name: "IX_RequestLine_RequestId",
                table: "RequestLine");

            migrationBuilder.DropIndex(
                name: "IX_Request_LaboratoryId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_MethodId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_MethodLine_MethodId",
                table: "MethodLine");

            migrationBuilder.DropColumn(
                name: "LaboratoryId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "MethodId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "MethodId",
                table: "MethodLine");
        }
    }
}
