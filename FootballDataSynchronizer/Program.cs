using System;
using System.IO;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Options;
using Integration.Synchronization;
using Integration.Synchronization.Abstract;
using Integration.Tools.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Serilog;
using Serilog.Events;

namespace SynchronizationRunner
{
    class Program
    {
        private static IConfigurationRoot _config;


        static void Main(string[] args)
        {

            _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceCollection = new ServiceCollection();
            var serviceProvider = ConfigureServices(serviceCollection);
            ConfigureLogger();
            serviceCollection.AddSingleton(_config);

            

            var synchronizer = serviceProvider.GetService<IGroupSynchronizer>();
            var group  = synchronizer.GetGroupFromStanding(new Standing {Group = "A"});

            Console.WriteLine($"Group name: {group.Name}");
            Console.ReadKey();

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
            

            //Options
            serviceCollection.Configure<FootballDataApiOptions>(_config.GetSection("footballDataApiOptions"));

            //Integration
            serviceCollection.AddScoped<IGroupSynchronizer, GroupSynchronizer>();
            serviceCollection.AddScoped<IApiHttpClient, IApiHttpClient>();
            serviceCollection.AddScoped<IStratosphereUnitOfWork, IStratosphereUnitOfWork>();
           
            var serviceProvider = serviceCollection.BuildServiceProvider();


            return serviceProvider;
        }

        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File("SynchLog.log", LogEventLevel.Debug, fileSizeLimitBytes: 5000000, rollOnFileSizeLimit: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console()
                .CreateLogger();
        
        }
    }
}
