using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf.Context.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeCanHaveManyAssignedRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requests_AssigneeId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AssignedRequestId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AssigneeId",
                table: "Requests",
                column: "AssigneeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requests_AssigneeId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "AssignedRequestId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AssigneeId",
                table: "Requests",
                column: "AssigneeId",
                unique: true);
        }
    }
}
