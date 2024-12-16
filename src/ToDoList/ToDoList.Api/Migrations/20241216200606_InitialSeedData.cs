using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoList.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "CompletionTime", "CreationTime", "Description", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Local), "Write up the API documentation and usage examples", false, "Complete project documentation" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 3, 9, 15, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 2, 14, 30, 0, 0, DateTimeKind.Local), "Review and merge outstanding PRs in the main repository", true, "Review pull requests" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), null, new DateTime(2024, 1, 3, 11, 0, 0, 0, DateTimeKind.Local), "Configure testing tools and write initial test cases", false, "Setup test environment" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));
        }
    }
}
