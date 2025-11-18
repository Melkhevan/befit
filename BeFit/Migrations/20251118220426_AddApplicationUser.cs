using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFit.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TrainingSessions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ExerciseRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_UserId",
                table: "TrainingSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseRecords_UserId",
                table: "ExerciseRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseRecords_AspNetUsers_UserId",
                table: "ExerciseRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_AspNetUsers_UserId",
                table: "TrainingSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseRecords_AspNetUsers_UserId",
                table: "ExerciseRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_AspNetUsers_UserId",
                table: "TrainingSessions");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSessions_UserId",
                table: "TrainingSessions");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseRecords_UserId",
                table: "ExerciseRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExerciseRecords");
        }
    }
}
