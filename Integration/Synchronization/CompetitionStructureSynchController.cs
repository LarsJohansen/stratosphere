using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Options;
using Integration.Synchronization.CompetitionStructure.Abstract;
using Integration.Tools;
using Integration.Tools.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.Synchronization
{
    public class CompetitionStructureSynchController : ICompetitionStructureSynchController
    {
        private readonly FootballDataApiOptions _apiOptions;
        private readonly ICompetitionSynchronizer _competitionSynchronizer;
        private readonly IGroupSynchronizer _groupSynchronizer;
        private readonly ITeamSynchronizer _teamSynchronizer;
        private readonly IApiHttpClient _apiHttpClient;
        private readonly ILogger _logger;
        

        public CompetitionStructureSynchController(IOptions<FootballDataApiOptions> apiOptions, ICompetitionSynchronizer competitionSynchronizer,
            IGroupSynchronizer groupSynchronizer, ITeamSynchronizer teamSynchronizer, IApiHttpClient apiHttpClient,
            ILogger<CompetitionStructureSynchController> logger) 
        {
            _apiOptions = apiOptions?.Value ?? throw new ArgumentNullException(nameof(apiOptions));
            _competitionSynchronizer = competitionSynchronizer ??
                                       throw new ArgumentNullException(nameof(competitionSynchronizer));
            _groupSynchronizer = groupSynchronizer ?? throw new ArgumentNullException(nameof(groupSynchronizer));
            _apiHttpClient = apiHttpClient ?? throw new ArgumentNullException(nameof(apiOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _teamSynchronizer = teamSynchronizer ?? throw new ArgumentNullException(nameof(teamSynchronizer));


        }

        public void Run(string leagueCategory, uint year)
        {
            _logger.LogDebug("Calling URL CompetitionEndpoint");
            var competitionUrl = $"{_apiOptions.BaseUri}{_apiOptions.CompetitionEndpoint}?season={year}";
           
            try
            {
                var competition = SynchCompetitionWithSetup(leagueCategory, competitionUrl);

                if (competition != null)
                {
                   
                    _groupSynchronizer.CreateUpdateGroups(competition);
                    _teamSynchronizer.CreateTeams(competition);

                }
          
            }
            catch (RestApiException restEx)
            {
                _logger.LogError(restEx,
                    $"Error returned from API:\nCode: {restEx.StatusCode}\n Message: {restEx.ReturnMessage}");
                throw;
            }
           
            _logger.LogDebug("Done synchronizing Competitions, CompetitionSetup, Groups and Teams");
            
        }

        private Competition SynchCompetitionWithSetup(string leagueCategory, string url)
        {
            _logger.LogDebug($"Fetching competitions from {url}");
            var competitions = _apiHttpClient.GetDeleteRequest<List<CompetitionDto>>(url, false, _apiOptions.HeaderCollection);

            _logger.LogDebug($"Got {competitions.Count} competitions");

            var competitionDto = competitions.SingleOrDefault(c => c.League == leagueCategory);

            if (competitionDto == null)
            {
                _logger.LogDebug($"No competition with leagueCategory {leagueCategory} found");
                return null;
            }

            _logger.LogDebug($"Competiton with id {competitionDto.Id} has leagueCategory {leagueCategory}." +
                             " Creating or updating competition and setup");

            var competition  = _competitionSynchronizer.CreateUpdateCompetition(competitionDto);
            
            return competition;

        }
    }
}
