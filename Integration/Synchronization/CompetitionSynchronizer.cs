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
       
        public CompetitionSynchronizer(IStratosphereUnitOfWork stratosphereUnitOfWork, IMapper mapper,
            ILogger<CompetitionSynchronizer> logger) : base(stratosphereUnitOfWork, mapper, logger)
        {

        }

        public SynchrnoizationResult CreateUpdateCompetition(CompetitionDto competitionDto)
        {

            var isNew = false; 
            var exisitingCompetition = StratosphereUnitOfWork.Competitions.SingleOrDefault(c => c.ExternalId == competitionDto.Id);

            if (exisitingCompetition == null)
            {
                CreateNewCompetition(competitionDto);
                isNew = true;
            }
            else
            {
                UpdateExistingCompetition(competitionDto, exisitingCompetition);
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

        private void CreateNewCompetition(CompetitionDto competitionDto)
        {
            var competition = Mapper.Map<CompetitionDto, Competition>(competitionDto);
            StratosphereUnitOfWork.Competitions.Add(competition);
        }
    }
}
