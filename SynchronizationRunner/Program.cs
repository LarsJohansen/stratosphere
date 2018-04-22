using System;
using System.IO;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Options;
using Integration.Synchronization;
using Integration.Synchronization.Abstract;
using Integration.Tools;
using Integration.Tools.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Abstract;
using Serilog;
using Serilog.Events;

namespace SynchronizationRunner
{
    class Program
    {
        private static IConfigurationRoot _config;
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            try
            {
                _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var serviceCollection = new ServiceCollection();
                _serviceProvider = ConfigureServices(serviceCollection);
                ConfigureLogger();
                serviceCollection.AddSingleton(_config);


                var syncController = _serviceProvider.GetService<ICompetitionStructureSynchController>();
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
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddSerilog()
            );
            serviceCollection.AddLogging();
            //3rd party
            serviceCollection.AddLogging();
            serviceCollection.AddAutoMapper();

            //DB
            var conString = Environment.GetEnvironmentVariable("StratosphereConnectionString");
            serviceCollection.AddDbContext<StratosphereContext>(
                options => options.UseMySql(conString));

            //Options
            serviceCollection.Configure<FootballDataApiOptions>(_config.GetSection("footballDataApiOptions"));

            //Integration
            serviceCollection.AddScoped<IGroupSynchronizer, GroupSynchronizer>();
            serviceCollection.AddScoped<IApiHttpClient, ApiHttpClient>();
            serviceCollection.AddScoped<IStratosphereUnitOfWork, StratosphereUnitOfWork>();
            serviceCollection.AddScoped<ICompetitionStructureSynchController, CompetitionStructureSynchController>();
            serviceCollection.AddScoped<ICompetitionSynchronizer, CompetitionSynchronizer>();
            serviceCollection.AddScoped<ICompetitionSetupSynchronizer, CompetitionSetupSynchronizer>();

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
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}