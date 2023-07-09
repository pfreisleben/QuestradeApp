using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ScoreApi.Infrastructure.Migrations
{
    public partial class AddScoreDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.UniqueConstraint("AK_Scores_UserId", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ScoreHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScoreId = table.Column<int>(type: "integer", nullable: false),
                    OccurrenceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreHistory_Occurrences_OccurrenceId",
                        column: x => x.OccurrenceId,
                        principalTable: "Occurrences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreHistory_Scores_ScoreId",
                        column: x => x.ScoreId,
                        principalTable: "Scores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoreHistory_OccurrenceId",
                table: "ScoreHistory",
                column: "OccurrenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreHistory_ScoreId",
                table: "ScoreHistory",
                column: "ScoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreHistory");

            migrationBuilder.DropTable(
                name: "Scores");
        }
    }
}
