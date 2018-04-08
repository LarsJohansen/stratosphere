using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persistence.Migrations
{
    public partial class addedmatchroundandday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "MatchDays",
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
                    table.PrimaryKey("PK_MatchDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchDays_MatchRounds_FkMatchRound",
                        column: x => x.FkMatchRound,
                        principalTable: "MatchRounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchDays_FkMatchRound",
                table: "MatchDays",
                column: "FkMatchRound");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRounds_FkCompetition",
                table: "MatchRounds",
                column: "FkCompetition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchDays");

            migrationBuilder.DropTable(
                name: "MatchRounds");
        }
    }
}
