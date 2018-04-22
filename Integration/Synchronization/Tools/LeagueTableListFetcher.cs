﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Options;
using Integration.Tools.Abstract;
using Microsoft.Extensions.Options;

namespace Integration.Synchronization.Tools
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

        public (LeagueTable, IEnumerable<IEnumerable<Standing>>) GetLeagueStandings(long competitionId)
        {
            var url = $"{_apiOptions.BaseUri}{_apiOptions.LeagueTableEndpoint.Replace("0", competitionId.ToString())}";
            var leagueTables = _apiHttpClient.GetDeleteRequest<LeagueTable>(url, false, _apiOptions.HeaderCollection);

            var enumerableProps = leagueTables.Standings.GetType().GetProperties().Where(p => p.PropertyType == typeof(List<Standing>));

           
            var standings = new List<List<Standing>>();
         

            foreach (var propertyInfo in enumerableProps)
            {
                var groupStanding = (List<Standing>)propertyInfo.GetValue(leagueTables.Standings, null);

                if (groupStanding?.Count > 0)
                {
                    standings.Add(groupStanding);
                }
            }

            return (leagueTables, standings);
        }
    }
}
