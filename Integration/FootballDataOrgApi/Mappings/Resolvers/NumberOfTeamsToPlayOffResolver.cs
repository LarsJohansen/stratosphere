using Integration.FootballDataOrgApi.FootballDataDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.Synchronization.Tools;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Mappings.Resolvers
{
    public class NumberOfTeamsToPlayOffResolver : IValueResolver<CompetitionDto, CompetitionSetup, int>
    {

        private readonly IStratosphereUnitOfWork _stratosphereUnitOfWork;
        private readonly ILogger _logger;

        public NumberOfTeamsToPlayOffResolver(IStratosphereUnitOfWork stratosphereUnitOfWork, ILogger<NumberOfTeamsToPlayOffResolver> logger)
        {
            _stratosphereUnitOfWork = stratosphereUnitOfWork ?? throw new ArgumentNullException(nameof(stratosphereUnitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public int Resolve(CompetitionDto source, CompetitionSetup destination, int destMember, ResolutionContext context)
        {
            
            var league = source.League;
            _logger.LogDebug($"Resolving number of teams to playoff for league {league}");

            var competitionRules =
                _stratosphereUnitOfWork.CompetitionRuleSets.SingleOrDefault(r => r.LeagueDescription == league);

            if (competitionRules == null)
            {
                _logger.LogDebug("No rules found, returning 0 teams to playoff");
                return 0;
            }

            var teamsToPlayOff = destination.NumberOfGroups * competitionRules.NumberOfTeamsToPlayOffPerGroup;

            _logger.LogDebug($"Rules found, returning {teamsToPlayOff} teams to playoff");

            return teamsToPlayOff;
            
        }
    }
  
}
