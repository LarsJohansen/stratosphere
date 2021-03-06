﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Persistence;
using System;

namespace Persistence.Migrations
{
    [DbContext(typeof(StratosphereContext))]
    [Migration("20180416181834_UserCompetiotionScore and UserMatchPrediction")]
    partial class UserCompetiotionScoreandUserMatchPrediction
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Persistence.Entities.Competition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedAt");

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<bool>("Enabled")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("Persistence.Entities.CompetitionSetup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkCompetition");

                    b.Property<int>("NumberOfGroups");

                    b.Property<int>("NumberOfTeams");

                    b.Property<int>("NumberOfTeamsToPlayOff");

                    b.HasKey("Id");

                    b.HasIndex("FkCompetition")
                        .IsUnique();

                    b.ToTable("CompetitionSetup");
                });

            modelBuilder.Entity("Persistence.Entities.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkCompetition");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("FkCompetition");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Persistence.Entities.Match", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("FkAwayStatistics");

                    b.Property<long>("FkAwayTeam");

                    b.Property<long?>("FkHomeStatistics");

                    b.Property<long>("FkHomeTeam");

                    b.Property<long>("FkMatchDay");

                    b.HasKey("Id");

                    b.HasIndex("FkAwayStatistics")
                        .IsUnique();

                    b.HasIndex("FkAwayTeam");

                    b.HasIndex("FkHomeStatistics")
                        .IsUnique();

                    b.HasIndex("FkHomeTeam");

                    b.HasIndex("FkMatchDay");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("Persistence.Entities.MatchDay", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkMatchRound");

                    b.Property<DateTime>("MatchDateTime");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("FkMatchRound");

                    b.ToTable("MatchDays");
                });

            modelBuilder.Entity("Persistence.Entities.MatchRound", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkCompetition");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("FkCompetition");

                    b.ToTable("MatchRounds");
                });

            modelBuilder.Entity("Persistence.Entities.MatchStatistics", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkTeam");

                    b.Property<bool>("HomeTeam");

                    b.Property<int>("NumberOfGoals");

                    b.Property<int>("NumberOfRedCards");

                    b.Property<int>("NumberOfYellowCards");

                    b.HasKey("Id");

                    b.HasIndex("FkTeam");

                    b.ToTable("MatchStatistics");
                });

            modelBuilder.Entity("Persistence.Entities.Team", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkCompetition");

                    b.Property<long>("FkGroup");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("FkCompetition");

                    b.HasIndex("FkGroup");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Persistence.Entities.TeamOnCompetition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkCompetition");

                    b.Property<long>("FkTeam");

                    b.Property<int?>("GroupTieBreakPosition");

                    b.HasKey("Id");

                    b.HasIndex("FkCompetition");

                    b.HasIndex("FkTeam");

                    b.ToTable("TeamOnCompetition");
                });

            modelBuilder.Entity("Persistence.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email")
                        .HasMaxLength(254);

                    b.Property<bool>("Enabled")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Firstname")
                        .HasMaxLength(100);

                    b.Property<string>("Lastname")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Persistence.Entities.UserCompetitionScore", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkCompetition");

                    b.Property<long>("FkUser");

                    b.Property<int>("Score");

                    b.HasKey("Id");

                    b.HasIndex("FkCompetition");

                    b.HasIndex("FkUser")
                        .IsUnique();

                    b.ToTable("UserCompetitionScore");
                });

            modelBuilder.Entity("Persistence.Entities.UserLeague", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkCompetition");

                    b.Property<long>("FkUserAdmin");

                    b.Property<string>("LeagueIdentifier")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("FkCompetition");

                    b.HasIndex("FkUserAdmin");

                    b.ToTable("UserLeagues");
                });

            modelBuilder.Entity("Persistence.Entities.UserMatchPrediction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkMatch");

                    b.Property<long>("FkUserOnLeague");

                    b.Property<int>("NumberOfGoalsAwayTeam");

                    b.Property<int>("NumberOfGoalsHomeTeam");

                    b.HasKey("Id");

                    b.HasIndex("FkMatch");

                    b.HasIndex("FkUserOnLeague");

                    b.ToTable("UserMatchPrediction");
                });

            modelBuilder.Entity("Persistence.Entities.UserOnLeague", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkUser");

                    b.Property<long>("FkUserLeague");

                    b.HasKey("Id");

                    b.HasIndex("FkUser");

                    b.HasIndex("FkUserLeague");

                    b.ToTable("UserOnLeagues");
                });

            modelBuilder.Entity("Persistence.Entities.UserTieBreak", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FkTeamOnCompetition");

                    b.Property<long>("FkUserOnLeague");

                    b.Property<uint>("TieBreakPosition");

                    b.HasKey("Id");

                    b.HasIndex("FkTeamOnCompetition");

                    b.HasIndex("FkUserOnLeague");

                    b.ToTable("UserTieBreak");
                });

            modelBuilder.Entity("Persistence.Entities.CompetitionSetup", b =>
                {
                    b.HasOne("Persistence.Entities.Competition", "Competition")
                        .WithOne("CompetitionSetup")
                        .HasForeignKey("Persistence.Entities.CompetitionSetup", "FkCompetition")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.Group", b =>
                {
                    b.HasOne("Persistence.Entities.Competition", "Competition")
                        .WithMany("Groups")
                        .HasForeignKey("FkCompetition")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.Match", b =>
                {
                    b.HasOne("Persistence.Entities.MatchStatistics", "AwayMatchStatistics")
                        .WithOne("AwayMatch")
                        .HasForeignKey("Persistence.Entities.Match", "FkAwayStatistics");

                    b.HasOne("Persistence.Entities.Team", "AwayTeam")
                        .WithMany("AwayMatches")
                        .HasForeignKey("FkAwayTeam")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Entities.MatchStatistics", "HomeMatchStatistics")
                        .WithOne("HomeMatch")
                        .HasForeignKey("Persistence.Entities.Match", "FkHomeStatistics");

                    b.HasOne("Persistence.Entities.Team", "HomeTeam")
                        .WithMany("HomeMatches")
                        .HasForeignKey("FkHomeTeam")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Entities.MatchDay", "MatchDay")
                        .WithMany("Matches")
                        .HasForeignKey("FkMatchDay")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.MatchDay", b =>
                {
                    b.HasOne("Persistence.Entities.MatchRound", "MatchRound")
                        .WithMany("MatchDays")
                        .HasForeignKey("FkMatchRound")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.MatchRound", b =>
                {
                    b.HasOne("Persistence.Entities.Competition", "Competition")
                        .WithMany("MatchRounds")
                        .HasForeignKey("FkCompetition")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.MatchStatistics", b =>
                {
                    b.HasOne("Persistence.Entities.Team", "Team")
                        .WithMany("MatchStatistics")
                        .HasForeignKey("FkTeam")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.Team", b =>
                {
                    b.HasOne("Persistence.Entities.Competition", "Competition")
                        .WithMany("Teams")
                        .HasForeignKey("FkCompetition")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Entities.Group", "Group")
                        .WithMany("Teams")
                        .HasForeignKey("FkGroup")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.TeamOnCompetition", b =>
                {
                    b.HasOne("Persistence.Entities.Competition", "Competition")
                        .WithMany("TeamOnCompetitions")
                        .HasForeignKey("FkCompetition")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Entities.Team", "Team")
                        .WithMany("TeamOnCompetitions")
                        .HasForeignKey("FkTeam")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.UserCompetitionScore", b =>
                {
                    b.HasOne("Persistence.Entities.Competition", "Competition")
                        .WithMany("UserCompetitionScores")
                        .HasForeignKey("FkCompetition")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Entities.User", "User")
                        .WithOne("UserCompetitionScore")
                        .HasForeignKey("Persistence.Entities.UserCompetitionScore", "FkUser")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.UserLeague", b =>
                {
                    b.HasOne("Persistence.Entities.Competition", "Competition")
                        .WithMany("UserLeagues")
                        .HasForeignKey("FkCompetition")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Entities.User", "UserAdmin")
                        .WithMany("UserLeaguesWhereAdmin")
                        .HasForeignKey("FkUserAdmin")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.UserMatchPrediction", b =>
                {
                    b.HasOne("Persistence.Entities.Match", "Match")
                        .WithMany("UserMatchPredictions")
                        .HasForeignKey("FkMatch")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Entities.UserOnLeague", "UserOnLeague")
                        .WithMany("UserMatchPredictions")
                        .HasForeignKey("FkUserOnLeague")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.UserOnLeague", b =>
                {
                    b.HasOne("Persistence.Entities.User", "User")
                        .WithMany("UserOnLeagues")
                        .HasForeignKey("FkUser")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Entities.UserLeague", "UserLeague")
                        .WithMany("UserOnLeagues")
                        .HasForeignKey("FkUserLeague")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Entities.UserTieBreak", b =>
                {
                    b.HasOne("Persistence.Entities.TeamOnCompetition", "TeamOnCompetition")
                        .WithMany("UserTieBreaks")
                        .HasForeignKey("FkTeamOnCompetition")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Entities.UserOnLeague", "UserOnLeague")
                        .WithMany("UserTieBreaks")
                        .HasForeignKey("FkUserOnLeague")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
