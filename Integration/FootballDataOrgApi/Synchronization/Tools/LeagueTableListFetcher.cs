using System;
using System.Collections.Generic;
using System.Linq;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Options;
using Integration.Tools.Abstract;
using Microsoft.Extensions.Options;

namespace Integration.FootballDataOrgApi.Synchronization.Tools
{
    public class LeagueTableGroupFetcher : ILeagueTableGroupFetcher
    {
        private readonly IApiHttpClient _apiHttpClient;
        private readonly FootballDataApiOptions _apiOptions;


        public LeagueTableGroupFetcher(IApiHttpClient apiHttpClient, IOptions<FootballDataApiOptions> options)
        {
            _apiHttpClient = apiHttpClient ?? throw new ArgumentNullException(nameof(apiHttpClient));
            _apiOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public (LeagueTableDto, IEnumerable<IEnumerable<StandingDto>>) GetLeagueStandings(long competitionId)
        {
            var url = $"{_apiOptions.BaseUri}{_apiOptions.LeagueTableEndpoint.Replace("0", competitionId.ToString())}";
            var leagueTables = _apiHttpClient.GetDeleteRequest<LeagueTableDto>(url, false, _apiOptions.HeaderCollection);

            var enumerableProps = leagueTables.Standings.GetType().GetProperties().Where(p => p.PropertyType == typeof(List<StandingDto>));

           
            var standings = new List<List<StandingDto>>();
         

            foreach (var propertyInfo in enumerableProps)
            {
                var groupStanding = (List<StandingDto>)propertyInfo.GetValue(leagueTables.Standings, null);

                if (groupStanding?.Count > 0)
                {
                    standings.Add(groupStanding);
                }
            }

            return (leagueTables, standings);
        }
    }
}
