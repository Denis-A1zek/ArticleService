using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleService.Database.Migrations
{
    /// <inheritdoc />
    public partial class rename_col : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_logs_articles_ArticleId",
                table: "article_logs");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "article_logs",
                newName: "article_id");

            migrationBuilder.RenameIndex(
                name: "IX_article_logs_ArticleId",
                table: "article_logs",
                newName: "IX_article_logs_article_id");

            migrationBuilder.AddForeignKey(
                name: "FK_article_logs_articles_article_id",
                table: "article_logs",
                column: "article_id",
                principalTable: "articles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_logs_articles_article_id",
                table: "article_logs");

            migrationBuilder.RenameColumn(
                name: "article_id",
                table: "article_logs",
                newName: "ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_article_logs_article_id",
                table: "article_logs",
                newName: "IX_article_logs_ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_article_logs_articles_ArticleId",
                table: "article_logs",
                column: "ArticleId",
                principalTable: "articles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
