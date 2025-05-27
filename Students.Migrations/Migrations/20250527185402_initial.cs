using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Students.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "Enrolments",
                columns: table => new
                {
                    EnrolmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Course = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolments", x => x.EnrolmentId);
                    table.ForeignKey(
                        name: "FK_Enrolments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "DateCreated", "DateModified", "FirstName", "Gender", "LastName" },
                values: new object[,]
                {
                    { 1, DateTime.UtcNow, DateTime.UtcNow, "John", 0, "Smith" },
                    { 2, DateTime.UtcNow, DateTime.UtcNow, "Aaron", 0, "Hanwell" },
                    { 3, DateTime.UtcNow, DateTime.UtcNow, "Quest", 0, "Ball" },
                    { 4, DateTime.UtcNow, DateTime.UtcNow, "Caroline", 1, "Turner" },
                    { 5, DateTime.UtcNow, DateTime.UtcNow, "David", 0, "Smith" },
                    { 6, DateTime.UtcNow, DateTime.UtcNow, "Clorie", 1, "Hanwell" },
                    { 7, DateTime.UtcNow, DateTime.UtcNow, "Pecca", 2, "Owell" },
                    { 8, DateTime.UtcNow, DateTime.UtcNow, "Bad", 1, "Apple" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Micah", "Davies" },
                    { 2, "Ben", "Foster" },
                    { 3, "Cameron", "Mason" },
                    { 4, "Zac", "Cooke" },
                    { 5, "Robbie", "Fox" }
                });

            migrationBuilder.InsertData(
                table: "Enrolments",
                columns: new[] { "EnrolmentId", "Course", "DateCreated", "DateModified", "Status", "StudentId" },
                values: new object[,]
                {
                    { 1, 3, DateTime.UtcNow, DateTime.UtcNow, 0, 1 },
                    { 2, 1, DateTime.UtcNow, DateTime.UtcNow, 0, 1 },
                    { 3, 1, DateTime.UtcNow, DateTime.UtcNow, 1, 2 },
                    { 4, 0, DateTime.UtcNow, DateTime.UtcNow, 0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StudentId",
                table: "Addresses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolments_StudentId",
                table: "Enrolments",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Enrolments");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
