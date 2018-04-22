using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Options;
using Integration.Synchronization.Abstract;
using Integration.Tools;
using Integration.Tools.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Abstract;

namespace Integration.Synchronization
{
    public class CompetitionStructureSynchController : ICompetitionStructureSynchController
    {
        private readonly FootballDataApiOptions _apiOptions;
        private readonly ICompetitionSynchronizer _competitionSynchronizer;
        private readonly IApiHttpClient _apiHttpClient;
        private readonly ILogger _logger;
        

        public CompetitionStructureSynchController(IOptions<FootballDataApiOptions> apiOptions, ICompetitionSynchronizer competitionSynchronizer, IApiHttpClient apiHttpClient,
            ILogger<BaseSynchronizer> logger) 
        {
            _apiOptions = apiOptions?.Value ?? throw new ArgumentNullException(nameof(apiOptions));
            _competitionSynchronizer = competitionSynchronizer ??
                                       throw new ArgumentNullException(nameof(competitionSynchronizer));
            _apiHttpClient = apiHttpClient ?? throw new ArgumentNullException(nameof(apiOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
           


        }

        public void Run(string leagueCategory, uint year)
        {
            
            var url = $"{_apiOptions.BaseUri}{_apiOptions.CompetitionEndpoint}?season={year}";
           
            try
            {
                _logger.LogDebug($"Fetching competitions from {url}");
                var competitions = _apiHttpClient.GetDeleteRequest<List<CompetitionDto>>(url, false, _apiOptions.HeaderCollection);

                _logger.LogDebug($"Got {competitions.Count} competitions");

                var competition = competitions.SingleOrDefault(c => c.League == leagueCategory);

                if (competition == null)
                {
                    _logger.LogDebug($"No competition with leagueCategory {leagueCategory} found");
                    return;
                }

                _logger.LogDebug($"Competiton with id {competition.Id} has leagueCategory {leagueCategory}." +  
                                 " Creating or updating competition and setup");

                var synchResult = _competitionSynchronizer.CreateUpdateCompetition(competition);

                _logger.LogDebug($"Create or Update resulted in {synchResult.ToString()}");

            }
            catch (RestApiException restEx)
            {
                _logger.LogError(restEx,
                    $"Error returned from API:\nCode: {restEx.StatusCode}\n Message: {restEx.ReturnMessage}");
                throw;
            }
           

            
        }
    }
}
