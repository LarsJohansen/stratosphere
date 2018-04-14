using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persistence.Migrations
{
    public partial class addedmatchandmatchstatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchStatistics",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkTeam = table.Column<long>(nullable: false),
                    HomeTeam = table.Column<bool>(nullable: false),
                    NumberOfGoals = table.Column<int>(nullable: false),
                    NumberOfRedCards = table.Column<int>(nullable: false),
                    NumberOfYellowCards = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchStatistics_Teams_FkTeam",
                        column: x => x.FkTeam,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkAwayStatistics = table.Column<long>(nullable: true),
                    FkAwayTeam = table.Column<long>(nullable: false),
                    FkHomeStatistics = table.Column<long>(nullable: true),
                    FkHomeTeam = table.Column<long>(nullable: false),
                    FkMatchDay = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_MatchStatistics_FkAwayStatistics",
                        column: x => x.FkAwayStatistics,
                        principalTable: "MatchStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Teams_FkAwayTeam",
                        column: x => x.FkAwayTeam,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Match_MatchStatistics_FkHomeStatistics",
                        column: x => x.FkHomeStatistics,
                        principalTable: "MatchStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Teams_FkHomeTeam",
                        column: x => x.FkHomeTeam,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Match_MatchDays_FkMatchDay",
                        column: x => x.FkMatchDay,
                        principalTable: "MatchDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_FkAwayStatistics",
                table: "Match",
                column: "FkAwayStatistics",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_FkAwayTeam",
                table: "Match",
                column: "FkAwayTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Match_FkHomeStatistics",
                table: "Match",
                column: "FkHomeStatistics",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_FkHomeTeam",
                table: "Match",
                column: "FkHomeTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Match_FkMatchDay",
                table: "Match",
                column: "FkMatchDay");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStatistics_FkTeam",
                table: "MatchStatistics",
                column: "FkTeam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "MatchStatistics");
        }
    }
}
