using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PythonLearn.DAL.Migrations
{
    /// <inheritdoc />
    public partial class many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Elements_ElementId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_LessonComments_LessonCommentId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Solutions_SolutionId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ElementId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_LessonCommentId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_SolutionId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ElementId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonCommentId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "SolutionId",
                table: "Lessons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElementId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonCommentId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SolutionId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ElementId",
                table: "Lessons",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LessonCommentId",
                table: "Lessons",
                column: "LessonCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SolutionId",
                table: "Lessons",
                column: "SolutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Elements_ElementId",
                table: "Lessons",
                column: "ElementId",
                principalTable: "Elements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_LessonComments_LessonCommentId",
                table: "Lessons",
                column: "LessonCommentId",
                principalTable: "LessonComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Solutions_SolutionId",
                table: "Lessons",
                column: "SolutionId",
                principalTable: "Solutions",
                principalColumn: "Id");
        }
    }
}
