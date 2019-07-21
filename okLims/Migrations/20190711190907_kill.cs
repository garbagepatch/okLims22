﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class kill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Request");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Request",
                nullable: false,
                defaultValue: false);
        }
    }
}
