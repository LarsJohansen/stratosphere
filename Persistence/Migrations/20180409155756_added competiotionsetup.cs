using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persistence.Migrations
{
    public partial class addedcompetiotionsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetitionSetup",
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
                    table.PrimaryKey("PK_CompetitionSetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionSetup_Competitions_FkCompetition",
                        column: x => x.FkCompetition,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionSetup_FkCompetition",
                table: "CompetitionSetup",
                column: "FkCompetition",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionSetup");
        }
    }
}
