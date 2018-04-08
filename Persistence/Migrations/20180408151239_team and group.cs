using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persistence.Migrations
{
    public partial class teamandgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLeague_Competition_FkCompetition",
                table: "UserLeague");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLeague_Users_FkUserAdmin",
                table: "UserLeague");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOnLeague_Users_FkUser",
                table: "UserOnLeague");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOnLeague_UserLeague_FkUserLeague",
                table: "UserOnLeague");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOnLeague",
                table: "UserOnLeague");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLeague",
                table: "UserLeague");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competition",
                table: "Competition");

            migrationBuilder.RenameTable(
                name: "UserOnLeague",
                newName: "UserOnLeagues");

            migrationBuilder.RenameTable(
                name: "UserLeague",
                newName: "UserLeagues");

            migrationBuilder.RenameTable(
                name: "Competition",
                newName: "Competitions");

            migrationBuilder.RenameIndex(
                name: "IX_UserOnLeague_FkUserLeague",
                table: "UserOnLeagues",
                newName: "IX_UserOnLeagues_FkUserLeague");

            migrationBuilder.RenameIndex(
                name: "IX_UserOnLeague_FkUser",
                table: "UserOnLeagues",
                newName: "IX_UserOnLeagues_FkUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserLeague_FkUserAdmin",
                table: "UserLeagues",
                newName: "IX_UserLeagues_FkUserAdmin");

            migrationBuilder.RenameIndex(
                name: "IX_UserLeague_FkCompetition",
                table: "UserLeagues",
                newName: "IX_UserLeagues_FkCompetition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOnLeagues",
                table: "UserOnLeagues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLeagues",
                table: "UserLeagues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competitions",
                table: "Competitions",
                column: "Id");

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
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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

            migrationBuilder.CreateIndex(
                name: "IX_Groups_FkCompetition",
                table: "Groups",
                column: "FkCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FkCompetition",
                table: "Teams",
                column: "FkCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FkGroup",
                table: "Teams",
                column: "FkGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLeagues_Competitions_FkCompetition",
                table: "UserLeagues",
                column: "FkCompetition",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLeagues_Users_FkUserAdmin",
                table: "UserLeagues",
                column: "FkUserAdmin",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOnLeagues_Users_FkUser",
                table: "UserOnLeagues",
                column: "FkUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOnLeagues_UserLeagues_FkUserLeague",
                table: "UserOnLeagues",
                column: "FkUserLeague",
                principalTable: "UserLeagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLeagues_Competitions_FkCompetition",
                table: "UserLeagues");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLeagues_Users_FkUserAdmin",
                table: "UserLeagues");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOnLeagues_Users_FkUser",
                table: "UserOnLeagues");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOnLeagues_UserLeagues_FkUserLeague",
                table: "UserOnLeagues");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOnLeagues",
                table: "UserOnLeagues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLeagues",
                table: "UserLeagues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competitions",
                table: "Competitions");

            migrationBuilder.RenameTable(
                name: "UserOnLeagues",
                newName: "UserOnLeague");

            migrationBuilder.RenameTable(
                name: "UserLeagues",
                newName: "UserLeague");

            migrationBuilder.RenameTable(
                name: "Competitions",
                newName: "Competition");

            migrationBuilder.RenameIndex(
                name: "IX_UserOnLeagues_FkUserLeague",
                table: "UserOnLeague",
                newName: "IX_UserOnLeague_FkUserLeague");

            migrationBuilder.RenameIndex(
                name: "IX_UserOnLeagues_FkUser",
                table: "UserOnLeague",
                newName: "IX_UserOnLeague_FkUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserLeagues_FkUserAdmin",
                table: "UserLeague",
                newName: "IX_UserLeague_FkUserAdmin");

            migrationBuilder.RenameIndex(
                name: "IX_UserLeagues_FkCompetition",
                table: "UserLeague",
                newName: "IX_UserLeague_FkCompetition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOnLeague",
                table: "UserOnLeague",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLeague",
                table: "UserLeague",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competition",
                table: "Competition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLeague_Competition_FkCompetition",
                table: "UserLeague",
                column: "FkCompetition",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLeague_Users_FkUserAdmin",
                table: "UserLeague",
                column: "FkUserAdmin",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOnLeague_Users_FkUser",
                table: "UserOnLeague",
                column: "FkUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOnLeague_UserLeague_FkUserLeague",
                table: "UserOnLeague",
                column: "FkUserLeague",
                principalTable: "UserLeague",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
