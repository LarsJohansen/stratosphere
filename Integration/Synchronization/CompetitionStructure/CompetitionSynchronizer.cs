using System;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.Synchronization.CompetitionStructure.Abstract;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.Synchronization.CompetitionStructure
{
    public class CompetitionSynchronizer : BaseSynchronizer, ICompetitionSynchronizer
    {
        private readonly ICompetitionSetupSynchronizer _competitionSetupSynchronizer;
        public CompetitionSynchronizer(ICompetitionSetupSynchronizer competitionSetupSynchronizer, IStratosphereUnitOfWork stratosphereUnitOfWork, IMapper mapper,
            ILogger<BaseSynchronizer> logger) : base(stratosphereUnitOfWork, mapper, logger)
        {
            _competitionSetupSynchronizer = competitionSetupSynchronizer ??
                                            throw new ArgumentNullException(nameof(competitionSetupSynchronizer));
        }

        public Competition CreateUpdateCompetition(CompetitionDto competitionDto)
        {

            var competition = StratosphereUnitOfWork.Competitions.SingleOrDefault(c => c.ExternalId == competitionDto.Id);

            if (competition == null)
            {
                Logger.LogDebug($"Could not find exisiting competition with ExternalId {competitionDto.Id}, creating new..");
                competition = CreateNewCompetition(competitionDto);
                _competitionSetupSynchronizer.CreateCompetitionSetup(competitionDto, competition.Id);
            }
            else
            {
                Logger.LogDebug($"Found exisiting competition with ExternalId {competitionDto.Id}, Entity id: {competition.Id}. Updating..");
                UpdateExistingCompetition(competitionDto, competition);
                _competitionSetupSynchronizer.UpdateExisitingCompetitionSetup(competitionDto, competition.Id);
            }
            
            StratosphereUnitOfWork.Complete();
            
            return competition;
           


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
