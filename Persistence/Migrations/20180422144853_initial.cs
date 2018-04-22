using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 254, nullable: true),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    Firstname = table.Column<string>(maxLength: 100, nullable: true),
                    Lastname = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionSetups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkCompetition = table.Column<long>(nullable: false),
                    NumberOfGroups = table.Column<int>(nullable: false),
                    NumberOfTeams = table.Column<int>(nullable: false),
                    NumberOfTeamsToPlayOff = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionSetups_Competitions_FkCompetition",
                        column: x => x.FkCompetition,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkCompetition = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Competitions_FkCompetition",
                        column: x => x.FkCompetition,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchRounds",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkCompetition = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchRounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchRounds_Competitions_FkCompetition",
                        column: x => x.FkCompetition,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompetitionScores",
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
                    table.PrimaryKey("PK_UserCompetitionScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCompetitionScores_Competitions_FkCompetition",
                        column: x => x.FkCompetition,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompetitionScores_Users_FkUser",
                        column: x => x.FkUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLeagues",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkCompetition = table.Column<long>(nullable: false),
                    FkUserAdmin = table.Column<long>(nullable: false),
                    LeagueIdentifier = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLeagues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLeagues_Competitions_FkCompetition",
                        column: x => x.FkCompetition,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLeagues_Users_FkUserAdmin",
                        column: x => x.FkUserAdmin,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CrestUri = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
                    FkCompetition = table.Column<long>(nullable: false),
                    FkGroup = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Competitions_FkCompetition",
                        column: x => x.FkCompetition,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Groups_FkGroup",
                        column: x => x.FkGroup,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkMatchRound = table.Column<long>(nullable: false),
                    MatchDateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchDay_MatchRounds_FkMatchRound",
                        column: x => x.FkMatchRound,
                        principalTable: "MatchRounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOnLeagues",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkUser = table.Column<long>(nullable: false),
                    FkUserLeague = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOnLeagues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOnLeagues_Users_FkUser",
                        column: x => x.FkUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOnLeagues_UserLeagues_FkUserLeague",
                        column: x => x.FkUserLeague,
                        principalTable: "UserLeagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "TeamOnCompetitions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkCompetition = table.Column<long>(nullable: false),
                    FkTeam = table.Column<long>(nullable: false),
                    GroupTieBreakPosition = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamOnCompetitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamOnCompetitions_Competitions_FkCompetition",
                        column: x => x.FkCompetition,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamOnCompetitions_Teams_FkTeam",
                        column: x => x.FkTeam,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
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
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_MatchStatistics_FkAwayStatistics",
                        column: x => x.FkAwayStatistics,
                        principalTable: "MatchStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_FkAwayTeam",
                        column: x => x.FkAwayTeam,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_MatchStatistics_FkHomeStatistics",
                        column: x => x.FkHomeStatistics,
                        principalTable: "MatchStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_FkHomeTeam",
                        column: x => x.FkHomeTeam,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_MatchDay_FkMatchDay",
                        column: x => x.FkMatchDay,
                        principalTable: "MatchDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTieBreaks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkTeamOnCompetition = table.Column<long>(nullable: false),
                    FkUserOnLeague = table.Column<long>(nullable: false),
                    TieBreakPosition = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTieBreaks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTieBreaks_TeamOnCompetitions_FkTeamOnCompetition",
                        column: x => x.FkTeamOnCompetition,
                        principalTable: "TeamOnCompetitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTieBreaks_UserOnLeagues_FkUserOnLeague",
                        column: x => x.FkUserOnLeague,
                        principalTable: "UserOnLeagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMatchPredictions",
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
                    table.PrimaryKey("PK_UserMatchPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMatchPredictions_Matches_FkMatch",
                        column: x => x.FkMatch,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMatchPredictions_UserOnLeagues_FkUserOnLeague",
                        column: x => x.FkUserOnLeague,
                        principalTable: "UserOnLeagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionSetups_FkCompetition",
                table: "CompetitionSetups",
                column: "FkCompetition",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_FkCompetition",
                table: "Groups",
                column: "FkCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_MatchDay_FkMatchRound",
                table: "MatchDay",
                column: "FkMatchRound");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FkAwayStatistics",
                table: "Matches",
                column: "FkAwayStatistics",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FkAwayTeam",
                table: "Matches",
                column: "FkAwayTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FkHomeStatistics",
                table: "Matches",
                column: "FkHomeStatistics",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FkHomeTeam",
                table: "Matches",
                column: "FkHomeTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FkMatchDay",
                table: "Matches",
                column: "FkMatchDay");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRounds_FkCompetition",
                table: "MatchRounds",
                column: "FkCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStatistics_FkTeam",
                table: "MatchStatistics",
                column: "FkTeam");

            migrationBuilder.CreateIndex(
                name: "IX_TeamOnCompetitions_FkCompetition",
                table: "TeamOnCompetitions",
                column: "FkCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_TeamOnCompetitions_FkTeam",
                table: "TeamOnCompetitions",
                column: "FkTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FkCompetition",
                table: "Teams",
                column: "FkCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FkGroup",
                table: "Teams",
                column: "FkGroup");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompetitionScores_FkCompetition",
                table: "UserCompetitionScores",
                column: "FkCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompetitionScores_FkUser",
                table: "UserCompetitionScores",
                column: "FkUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLeagues_FkCompetition",
                table: "UserLeagues",
                column: "FkCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_UserLeagues_FkUserAdmin",
                table: "UserLeagues",
                column: "FkUserAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_UserMatchPredictions_FkMatch",
                table: "UserMatchPredictions",
                column: "FkMatch");

            migrationBuilder.CreateIndex(
                name: "IX_UserMatchPredictions_FkUserOnLeague",
                table: "UserMatchPredictions",
                column: "FkUserOnLeague");

            migrationBuilder.CreateIndex(
                name: "IX_UserOnLeagues_FkUser",
                table: "UserOnLeagues",
                column: "FkUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserOnLeagues_FkUserLeague",
                table: "UserOnLeagues",
                column: "FkUserLeague");

            migrationBuilder.CreateIndex(
                name: "IX_UserTieBreaks_FkTeamOnCompetition",
                table: "UserTieBreaks",
                column: "FkTeamOnCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_UserTieBreaks_FkUserOnLeague",
                table: "UserTieBreaks",
                column: "FkUserOnLeague");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionSetups");

            migrationBuilder.DropTable(
                name: "UserCompetitionScores");

            migrationBuilder.DropTable(
                name: "UserMatchPredictions");

            migrationBuilder.DropTable(
                name: "UserTieBreaks");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "TeamOnCompetitions");

            migrationBuilder.DropTable(
                name: "UserOnLeagues");

            migrationBuilder.DropTable(
                name: "MatchStatistics");

            migrationBuilder.DropTable(
                name: "MatchDay");

            migrationBuilder.DropTable(
                name: "UserLeagues");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "MatchRounds");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Competitions");
        }
    }
}
