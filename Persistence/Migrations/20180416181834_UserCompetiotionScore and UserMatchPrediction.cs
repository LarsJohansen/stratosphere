using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persistence.Migrations
{
    public partial class UserCompetiotionScoreandUserMatchPrediction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCompetitionScore",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkCompetition = table.Column<long>(nullable: false),
                    FkUser = table.Column<long>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompetitionScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCompetitionScore_Competitions_FkCompetition",
                        column: x => x.FkCompetition,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompetitionScore_Users_FkUser",
                        column: x => x.FkUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMatchPrediction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkMatch = table.Column<long>(nullable: false),
                    FkUserOnLeague = table.Column<long>(nullable: false),
                    NumberOfGoalsAwayTeam = table.Column<int>(nullable: false),
                    NumberOfGoalsHomeTeam = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMatchPrediction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMatchPrediction_Match_FkMatch",
                        column: x => x.FkMatch,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMatchPrediction_UserOnLeagues_FkUserOnLeague",
                        column: x => x.FkUserOnLeague,
                        principalTable: "UserOnLeagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCompetitionScore_FkCompetition",
                table: "UserCompetitionScore",
                column: "FkCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompetitionScore_FkUser",
                table: "UserCompetitionScore",
                column: "FkUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMatchPrediction_FkMatch",
                table: "UserMatchPrediction",
                column: "FkMatch");

            migrationBuilder.CreateIndex(
                name: "IX_UserMatchPrediction_FkUserOnLeague",
                table: "UserMatchPrediction",
                column: "FkUserOnLeague");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCompetitionScore");

            migrationBuilder.DropTable(
                name: "UserMatchPrediction");
        }
    }
}
