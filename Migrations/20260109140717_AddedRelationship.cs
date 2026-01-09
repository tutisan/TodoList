using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_AuthorId",
                table: "TaskItems",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Accounts_AuthorId",
                table: "TaskItems",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Accounts_AuthorId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_AuthorId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "TaskItems");
        }
    }
}
