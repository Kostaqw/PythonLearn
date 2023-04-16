using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PythonLearn.DAL.Migrations
{
    /// <inheritdoc />
    public partial class article : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Titles_TitleId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_TitleId",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Titles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Titles_ArticleId",
                table: "Titles",
                column: "ArticleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Articles_ArticleId",
                table: "Titles",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Articles_ArticleId",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_Titles_ArticleId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Titles");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TitleId",
                table: "Articles",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Titles_TitleId",
                table: "Articles",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
