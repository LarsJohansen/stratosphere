using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.Synchronization.Abstract;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.Synchronization
{
    public class CompetitionSynchronizer : BaseSynchronizer, ICompetitionSynchronizer
    {
        private readonly ICompetitionSetupSynchronizer _competitionSetupSynchronizer;
        public CompetitionSynchronizer(ICompetitionSetupSynchronizer competitionSetupSynchronizer, IStratosphereUnitOfWork stratosphereUnitOfWork, IMapper mapper,
            ILogger<CompetitionSynchronizer> logger) : base(stratosphereUnitOfWork, mapper, logger)
        {
            _competitionSetupSynchronizer = competitionSetupSynchronizer ??
                                            throw new ArgumentNullException(nameof(competitionSetupSynchronizer));
        }

        public SynchrnoizationResult CreateUpdateCompetition(CompetitionDto competitionDto)
        {

            var isNew = false; 
            var exisitingCompetition = StratosphereUnitOfWork.Competitions.SingleOrDefault(c => c.ExternalId == competitionDto.Id);

            if (exisitingCompetition == null)
            {
                Logger.LogDebug($"Could not find exisiting competition with ExternalId {competitionDto.Id}, creating new..");
                isNew = true;
                var competition = CreateNewCompetition(competitionDto);
                _competitionSetupSynchronizer.CreateCompetitionSetup(competitionDto, competition.Id);
            }
            else
            {
                Logger.LogDebug($"Found exisiting competition with ExternalId {competitionDto.Id}, Entity id: {exisitingCompetition.Id}. Updating..");
                UpdateExistingCompetition(competitionDto, exisitingCompetition);
                _competitionSetupSynchronizer.UpdateExisitingCompetitionSetup(competitionDto, exisitingCompetition.Id);
            }
            
            StratosphereUnitOfWork.Complete();
            
            return (isNew) ? SynchrnoizationResult.CreatedNew : SynchrnoizationResult.UpdatedExisting;
           


        }

        private void UpdateExistingCompetition(CompetitionDto competitionDto, Competition competition)
        {
            var newCompetition = Mapper.Map<CompetitionDto, Competition>(competitionDto);
            competition.Description = newCompetition.Name;
            competition.Description = newCompetition.Description;
            
        }

        private Competition CreateNewCompetition(CompetitionDto competitionDto)
        {
            var competition = Mapper.Map<CompetitionDto, Competition>(competitionDto);
            StratosphereUnitOfWork.Competitions.Add(competition);
            StratosphereUnitOfWork.Complete();
            return competition;
        }
    }
}
