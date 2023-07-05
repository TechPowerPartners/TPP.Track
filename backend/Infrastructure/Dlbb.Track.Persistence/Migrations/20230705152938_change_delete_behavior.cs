using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dlbb.Track.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class change_delete_behavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AppUsers_AppUserId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AppUsers_AppUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Activities_ActivityId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AppUsers_AppUserId",
                table: "Sessions");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AppUsers_AppUserId",
                table: "Activities",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AppUsers_AppUserId",
                table: "Categories",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Activities_ActivityId",
                table: "Sessions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AppUsers_AppUserId",
                table: "Sessions",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AppUsers_AppUserId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AppUsers_AppUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Activities_ActivityId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AppUsers_AppUserId",
                table: "Sessions");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AppUsers_AppUserId",
                table: "Activities",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AppUsers_AppUserId",
                table: "Categories",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Activities_ActivityId",
                table: "Sessions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AppUsers_AppUserId",
                table: "Sessions",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
