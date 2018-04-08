using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persistence.Migrations
{
    public partial class relationsbetweenuseruseronleagueandcompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserOnLeague",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FkUser = table.Column<long>(nullable: false),
                    FkUserLeague = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOnLeague", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOnLeague_Users_FkUser",
                        column: x => x.FkUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOnLeague_UserLeague_FkUserLeague",
                        column: x => x.FkUserLeague,
                        principalTable: "UserLeague",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOnLeague_FkUser",
                table: "UserOnLeague",
                column: "FkUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserOnLeague_FkUserLeague",
                table: "UserOnLeague",
                column: "FkUserLeague");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOnLeague");
        }
    }
}
