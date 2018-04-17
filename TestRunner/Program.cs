using System;
using System.Collections.Generic;
using Integration.Models;
using Integration.Tools;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = Environment.GetEnvironmentVariable("FOOTBALLDATAAPIKEY");

            var headerDictionary = new Dictionary<string, string> {{"x-auth-token", apiKey}};

            var apiHttpClient = new ApiHttpClient();
            var competitionDtos = apiHttpClient.GetDeleteRequest<List<CompetitionDto>>("http://api.football-data.org/v1/competitions/?season=2018",
                false, headerDictionary);


            foreach (var competitionDto in competitionDtos)
            {
                Console.WriteLine($"Id: {competitionDto.Id}\nCaption: {competitionDto.Caption}\nLeaague: {competitionDto.League}\nNumberOfTeams\n{competitionDto.NumberOfTeams}\nNumberOfGames\n{competitionDto.NumberOfGames}\nYear:{competitionDto.Year}\n");
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
