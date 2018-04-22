using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Options;
using Integration.Tools.Abstract;
using Microsoft.Extensions.Options;
using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Mappings.Resolvers
{
   
    public class NumberOfGroupsInCompetitionResolver : IValueResolver<CompetitionDto, CompetitionSetup, int>
    {
        private readonly IApiHttpClient _apiHttpClient;
        private readonly FootballDataApiOptions _apiOptions;

        public NumberOfGroupsInCompetitionResolver(IApiHttpClient apiHttpClient, IOptions<FootballDataApiOptions> options)
        {
            _apiHttpClient = apiHttpClient ?? throw new ArgumentNullException(nameof(apiHttpClient));
            _apiOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

   

        public int Resolve(CompetitionDto source, CompetitionSetup destination, int destMember, ResolutionContext context)
        {
            var url = $"{_apiOptions.BaseUri}{_apiOptions.LeagueTableEndpoint.Replace("0", source.Id.ToString())}";
            var leagueTables = _apiHttpClient.GetDeleteRequest<LeagueTable>(url, false, _apiOptions.HeaderCollection);

            var numberOfGroups = 0;

            var enumerableProps = leagueTables.Standings.GetType().GetProperties().Where(p => p.PropertyType == typeof(List<Standing>));

            foreach (var propertyInfo in enumerableProps)
            {
                var groupStanding = (List<Standing>) propertyInfo.GetValue(source, null);

                if (groupStanding?.Count > 0)
                {
                    numberOfGroups++;
                }
            }

            return numberOfGroups;
        }
    }
}
