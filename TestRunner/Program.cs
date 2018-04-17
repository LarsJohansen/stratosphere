using System;
using System.Collections.Generic;
using Integration.FootballDataDto;
using Integration.Models;
using Integration.Tools;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("FOOTBALLDATAAPIKEY");

                var headerDictionary = new Dictionary<string, string> {{"x-auth-token", apiKey}};

                var apiHttpClient = new ApiHttpClient();
                var competitionDtos = apiHttpClient.GetDeleteRequest<List<CompetitionDto>>(
                    "http://api.football-data.org/v1/competitions/?season=2018",
                    false, headerDictionary);

                long worldCupId = -1;

                foreach (var competitionDto in competitionDtos)
                {
                    Console.WriteLine(
                        $"Id: {competitionDto.Id}\nCaption: {competitionDto.Caption}\nLeaague: {competitionDto.League}" +
                        $"\nNumberOfTeams\n{competitionDto.NumberOfTeams}\nNumberOfGames\n{competitionDto.NumberOfGames}\nYear:{competitionDto.Year}\n");
                    Console.WriteLine();

                    if (competitionDto.Caption.Contains("World Cup"))
                    {
                        worldCupId = competitionDto.Id;
                    }
                }

                if (worldCupId != -1)
                {
                    Console.WriteLine();

                    Console.WriteLine($"*** World Cup has Id {worldCupId} ***");

                    var teams = apiHttpClient.GetDeleteRequest<TeamsDto>(
                        $"http://api.football-data.org/v1/competitions/{worldCupId}/teams", false, headerDictionary);

                    foreach (var teamDto in teams.Teams)
                    {
                        Console.WriteLine($"Id: {teamDto.Id}\nName: {teamDto.Name}\nShortName: {teamDto.ShortName}");
                    }
                }

               
            }
            catch (RestApiException restEx)
            {
                Console.WriteLine($"RestApiException Code: {restEx.ReturnCode} - Message: {restEx.Message}");
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
    }
}
