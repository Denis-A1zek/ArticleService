using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArticleService.Database.Migrations
{
    /// <inheritdoc />
    public partial class rename_table_users_info : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_token_users_UserId",
                table: "users_token");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("06623b29-6c65-7449-8000-814f6d606bd7"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("06623b29-6c65-750b-8000-d1fa800b0302"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("06623b29-6c65-7512-8000-853a0188cf59"));

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users_token",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_token_UserId",
                table: "users_token",
                newName: "IX_users_token_user_id");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("06623b31-d7b6-7f45-8000-1f8b7eddab1b"), "User" },
                    { new Guid("06623b31-d7b6-7f7c-8000-d114ad18ef47"), "Admin" },
                    { new Guid("06623b31-d7b6-7f7f-8000-9efc35546589"), "Moderator" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_users_token_users_user_id",
                table: "users_token",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_token_users_user_id",
                table: "users_token");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("06623b31-d7b6-7f45-8000-1f8b7eddab1b"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("06623b31-d7b6-7f7c-8000-d114ad18ef47"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("06623b31-d7b6-7f7f-8000-9efc35546589"));

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "users_token",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_users_token_user_id",
                table: "users_token",
                newName: "IX_users_token_UserId");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("06623b29-6c65-7449-8000-814f6d606bd7"), "User" },
                    { new Guid("06623b29-6c65-750b-8000-d1fa800b0302"), "Admin" },
                    { new Guid("06623b29-6c65-7512-8000-853a0188cf59"), "Moderator" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_users_token_users_UserId",
                table: "users_token",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
