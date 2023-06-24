using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dlbb.Track.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_relationship_sessions_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AppUsers_AppUserId",
                table: "Sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "Sessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AppUsers_AppUserId",
                table: "Sessions",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AppUsers_AppUserId",
                table: "Sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "Sessions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AppUsers_AppUserId",
                table: "Sessions",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
