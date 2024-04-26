using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentDataDB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentJoinDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marksheets",
                columns: table => new
                {
                    MarksheetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MathsMarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScienceMarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalMarks = table.Column<int>(type: "int", nullable: false),
                    MarksObtained = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marksheets", x => x.MarksheetId);
                    table.ForeignKey(
                        name: "FK_Marksheets_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marksheets_StudentId",
                table: "Marksheets",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marksheets");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
