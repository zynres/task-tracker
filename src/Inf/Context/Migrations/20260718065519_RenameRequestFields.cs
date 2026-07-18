using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf.Context.Migrations
{
    /// <inheritdoc />
    public partial class RenameRequestFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Employees_PerformerId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Requests",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "PerformerId",
                table: "Requests",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_PerformerId",
                table: "Requests",
                newName: "IX_Requests_AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Employees_AssigneeId",
                table: "Requests",
                column: "AssigneeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Employees_AssigneeId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Requests",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "Requests",
                newName: "PerformerId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_AssigneeId",
                table: "Requests",
                newName: "IX_Requests_PerformerId");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Employees_PerformerId",
                table: "Requests",
                column: "PerformerId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
