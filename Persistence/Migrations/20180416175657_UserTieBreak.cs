using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persistence.Migrations
{
    public partial class UserTieBreak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTieBreak",
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
                    table.PrimaryKey("PK_UserTieBreak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTieBreak_TeamOnCompetition_FkTeamOnCompetition",
                        column: x => x.FkTeamOnCompetition,
                        principalTable: "TeamOnCompetition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTieBreak_UserOnLeagues_FkUserOnLeague",
                        column: x => x.FkUserOnLeague,
                        principalTable: "UserOnLeagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTieBreak_FkTeamOnCompetition",
                table: "UserTieBreak",
                column: "FkTeamOnCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_UserTieBreak_FkUserOnLeague",
                table: "UserTieBreak",
                column: "FkUserOnLeague");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTieBreak");
        }
    }
}
