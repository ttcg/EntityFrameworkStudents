using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Repository.Migrations
{
    /// <inheritdoc />
    public partial class StudentAuditData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: DateTime.UtcNow);            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Students");
        }
    }
}
