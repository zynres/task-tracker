using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestCompletedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignetRequestId",
                table: "Employees",
                newName: "AssignedRequestId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Requests",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "Requests",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "AssignedRequestId",
                table: "Employees",
                newName: "AssignetRequestId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Requests",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
