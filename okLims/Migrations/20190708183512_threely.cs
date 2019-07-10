using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace okLims.Migrations
{
    public partial class threely : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Laboratory_LaboratoryId",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "DateCompleted",
                table: "RequestLine",
                newName: "SpecialDetails");

            migrationBuilder.AddColumn<int>(
                name: "ControllerID",
                table: "RequestLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FilterID",
                table: "RequestLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LaboratoryId",
                table: "RequestLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RequesterEmail",
                table: "RequestLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeID",
                table: "RequestLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LaboratoryId",
                table: "Request",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LaboratoryName",
                table: "Laboratory",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Laboratory_LaboratoryId",
                table: "Request",
                column: "LaboratoryId",
                principalTable: "Laboratory",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Laboratory_LaboratoryId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ControllerID",
                table: "RequestLine");

            migrationBuilder.DropColumn(
                name: "FilterID",
                table: "RequestLine");

            migrationBuilder.DropColumn(
                name: "LaboratoryId",
                table: "RequestLine");

            migrationBuilder.DropColumn(
                name: "RequesterEmail",
                table: "RequestLine");

            migrationBuilder.DropColumn(
                name: "SizeID",
                table: "RequestLine");

            migrationBuilder.RenameColumn(
                name: "SpecialDetails",
                table: "RequestLine",
                newName: "DateCompleted");

            migrationBuilder.AlterColumn<int>(
                name: "LaboratoryId",
                table: "Request",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "LaboratoryName",
                table: "Laboratory",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Laboratory_LaboratoryId",
                table: "Request",
                column: "LaboratoryId",
                principalTable: "Laboratory",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
