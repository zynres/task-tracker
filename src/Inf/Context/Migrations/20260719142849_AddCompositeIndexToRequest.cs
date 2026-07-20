using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddCompositeIndexToRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requests_AssigneeId",
                table: "Requests");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AssigneeId_Status_DeadLine",
                table: "Requests",
                columns: new[] { "AssigneeId", "Status", "DeadLine" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requests_AssigneeId_Status_DeadLine",
                table: "Requests");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AssigneeId",
                table: "Requests",
                column: "AssigneeId");
        }
    }
}
