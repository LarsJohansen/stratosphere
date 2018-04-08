using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence
{
    public class StratosphereContext : DbContext
    {
        public StratosphereContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserOnLeague> UserOnLeagues { get; set; }

        public DbSet<UserLeague> UserLeagues { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Team> Teams { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Firstname)
                    .HasMaxLength(100);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(254);

                entity.Property(e => e.Enabled)
                    .HasDefaultValue(true);

                
            });

            modelBuilder.Entity<UserLeague>(entity =>
            {
                entity.Property(e => e.LeagueIdentifier)
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .HasMaxLength(100);


                entity.HasOne(e => e.Competition)
                    .WithMany(c => c.UserLeagues)
                    .HasForeignKey(e => e.FkCompetition);

                entity.HasOne(e => e.UserAdmin)
                    .WithMany(u => u.UserLeaguesWhereAdmin)
                    .HasForeignKey(e => e.FkUserAdmin);
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasMaxLength(250);

                entity.Property(e => e.Enabled)
                    .HasDefaultValue(true);

            });

            modelBuilder.Entity<UserOnLeague>(entity =>
            {
                entity.HasOne(e => e.UserLeague)
                    .WithMany(ul => ul.UserOnLeagues)
                    .HasForeignKey(e => e.FkUserLeague);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserOnLeagues)
                    .HasForeignKey(e => e.FkUser);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100);

                entity.HasOne(e => e.Group)
                    .WithMany(g => g.Teams)
                    .HasForeignKey(e => e.FkGroup);

                entity.HasOne(e => e.Competition)
                    .WithMany(g => g.Teams)
                    .HasForeignKey(e => e.FkCompetition);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100);

                entity.HasOne(e => e.Competition)
                    .WithMany(e => e.Groups)
                    .HasForeignKey(e => e.FkCompetition);
            });

        }
    }
}
