using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Options;
using Integration.Synchronization.Tools;
using Integration.Tools.Abstract;
using Microsoft.Extensions.Options;
using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Mappings.Resolvers
{
   
    public class NumberOfGroupsInCompetitionResolver : IValueResolver<CompetitionDto, CompetitionSetup, int>
    {
    
        private readonly ILeagueTableGroupFetcher _leagueTableGroupFetcher;

        public NumberOfGroupsInCompetitionResolver(ILeagueTableGroupFetcher leagueTableGroupFetcher)
        {
            _leagueTableGroupFetcher = leagueTableGroupFetcher ?? throw new ArgumentNullException(nameof(leagueTableGroupFetcher));
          
        }

   
        public int Resolve(CompetitionDto source, CompetitionSetup destination, int destMember, ResolutionContext context)
        {
            var (leagueTables, allGroupStandings) = _leagueTableGroupFetcher.GetLeagueStandings(source.Id);

            var numberOfGroups = 0;

            foreach (var groupStanding in allGroupStandings)
            {
 
                if (groupStanding?.Count() > 0)
                {
                    numberOfGroups++;
                }
            }

            return numberOfGroups;
        }
    }
}
