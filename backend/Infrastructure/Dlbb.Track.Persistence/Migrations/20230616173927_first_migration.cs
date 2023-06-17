using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dlbb.Track.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class first_migration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Activities",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: false),
					Description = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Activities", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Sessions",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Duration = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
					StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					ActivityId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Sessions", x => x.Id);
					table.ForeignKey(
						name: "FK_Sessions_Activities_ActivityId",
						column: x => x.ActivityId,
						principalTable: "Activities",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Activities_Id",
				table: "Activities",
				column: "Id",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Sessions_ActivityId",
				table: "Sessions",
				column: "ActivityId");

			migrationBuilder.CreateIndex(
				name: "IX_Sessions_Id",
				table: "Sessions",
				column: "Id",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Sessions");

			migrationBuilder.DropTable(
				name: "Activities");
		}
	}
}
