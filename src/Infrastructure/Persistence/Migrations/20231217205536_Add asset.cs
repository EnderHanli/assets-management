using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addasset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScheduleControlTime",
                table: "Assets",
                newName: "ScheduleControlTimeId");

            migrationBuilder.CreateTable(
                name: "ControlTimeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlTimeTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ScheduleControlTimeId",
                table: "Assets",
                column: "ScheduleControlTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ControlTimeTypes_ScheduleControlTimeId",
                table: "Assets",
                column: "ScheduleControlTimeId",
                principalTable: "ControlTimeTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ControlTimeTypes_ScheduleControlTimeId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "ControlTimeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ScheduleControlTimeId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "ScheduleControlTimeId",
                table: "Assets",
                newName: "ScheduleControlTime");
        }
    }
}
