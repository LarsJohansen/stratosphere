using System;
using System.Collections.Generic;
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
        private readonly IApiHttpClient _apiHttpClient;
        private readonly ILogger _logger;

        public CompetitionStructureSynchController(IOptions<FootballDataApiOptions> apiOptions, IApiHttpClient apiHttpClient,
            ILogger<BaseSynchronizer> logger) 
        {
            _apiOptions = apiOptions?.Value ?? throw new ArgumentNullException(nameof(apiOptions));
            _apiHttpClient = apiHttpClient ?? throw new ArgumentNullException(nameof(apiOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public void Run(string leagueCategory, uint year)
        {
            
            var url = $"{_apiOptions.BaseUri}{_apiOptions.CompetitionEndpoint}/{year}";
            //TODO: Add auth header somehow (static?)
            try
            {
                var competition = _apiHttpClient.GetDeleteRequest<CompetitionDto>(url, false);


            }
            catch (RestApiException restEx)
            {
                _logger.LogError(restEx,
                    $"Error returned from API:\nCode: {restEx.ReturnCode}\n Message: {restEx.ReturnMessage}");
                throw;
            }
           

            
        }
    }
}
