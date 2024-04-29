using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArticleService.Database.Migrations
{
    /// <inheritdoc />
    public partial class rename_table_created_at_updated_at_user_tabke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "UserTokenId",
                table: "users",
                newName: "user_token_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "upated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("06623b38-27d0-7f7b-8000-cf1f44f3cf66"), "User" },
                    { new Guid("06623b38-27d1-7043-8000-05c7d5460485"), "Admin" },
                    { new Guid("06623b38-27d1-7048-8000-281ca5d16541"), "Moderator" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("06623b38-27d0-7f7b-8000-cf1f44f3cf66"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("06623b38-27d1-7043-8000-05c7d5460485"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("06623b38-27d1-7048-8000-281ca5d16541"));

            migrationBuilder.RenameColumn(
                name: "user_token_id",
                table: "users",
                newName: "UserTokenId");

            migrationBuilder.RenameColumn(
                name: "upated_at",
                table: "users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("06623b31-d7b6-7f45-8000-1f8b7eddab1b"), "User" },
                    { new Guid("06623b31-d7b6-7f7c-8000-d114ad18ef47"), "Admin" },
                    { new Guid("06623b31-d7b6-7f7f-8000-9efc35546589"), "Moderator" }
                });
        }
    }
}
