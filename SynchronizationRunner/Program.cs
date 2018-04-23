﻿using System;
using System.IO;
using System.Linq;
using AutoMapper;
using Integration;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Options;
using Integration.FootballDataOrgApi.Synchronization;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract;
using Integration.FootballDataOrgApi.Synchronization.Tools;
using Integration.Tools;
using Integration.Tools.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Abstract;
using Persistence.Entities;
using Serilog;
using Serilog.Events;

namespace SynchronizationRunner
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                var serviceCollection = new ServiceCollection();
                var serviceProvider = ConfigureServices(serviceCollection);
                Initialize(serviceProvider);
                var syncController = serviceProvider.GetService<ICompetitionStructureSynchController>();
                syncController.Run("WC", 2018);

       
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static IServiceProvider ConfigureServices(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Add console logging
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole(configuration.GetSection("Logging"))
                .AddSerilog()
                .AddDebug());
            serviceCollection.AddLogging();

            // Add Serilog logging           
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.RollingFile(configuration["Serilog:LogFile"])
                .CreateLogger();

            serviceCollection.AddSingleton(configuration);
        
       
            serviceCollection.AddAutoMapper();

            //DB
            var conString = Environment.GetEnvironmentVariable("StratosphereConnectionString");
            serviceCollection.AddDbContext<StratosphereContext>(
                options => options.UseMySql(conString));

            //Options
            serviceCollection.Configure<FootballDataApiOptions>(configuration.GetSection("footballDataApiOptions"));

            //Integration
            serviceCollection.AddScoped<IGroupSynchronizer, GroupSynchronizer>();
            serviceCollection.AddScoped<IApiHttpClient, ApiHttpClient>();
            serviceCollection.AddScoped<IStratosphereUnitOfWork, StratosphereUnitOfWork>();
            serviceCollection.AddScoped<ICompetitionStructureSynchController, CompetitionStructureSynchController>();
            serviceCollection.AddScoped<ICompetitionSynchronizer, CompetitionSynchronizer>();
            serviceCollection.AddScoped<ICompetitionSetupSynchronizer, CompetitionSetupSynchronizer>();
            serviceCollection.AddScoped<ILeagueTableGroupFetcher, LeagueTableGroupFetcher>();
            serviceCollection.AddScoped<ITeamSynchronizer, TeamSynchronizer>();
            var serviceProvider = serviceCollection.BuildServiceProvider();


            return serviceProvider;
        }

        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File("SynchLog.log", LogEventLevel.Debug, fileSizeLimitBytes: 5000000,
                    rollOnFileSizeLimit: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();
        }


        private static void Initialize(IServiceProvider service)
        {
            using (var serviceScope = service.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<StratosphereContext>();
                db.Database.EnsureCreated();
                InsertDefaultData(db);
            }
        }

        private static void InsertDefaultData(StratosphereContext context)
        {
            if (context.CompetitionRuleSets.Any())
            {
                return;
            }

            var rule = new CompetitionRuleSet {LeagueDescription = "WC", NumberOfTeamsToPlayOffPerGroup = 2};
            context.CompetitionRuleSets.Add(rule);
            context.SaveChanges();
        }
    }
}