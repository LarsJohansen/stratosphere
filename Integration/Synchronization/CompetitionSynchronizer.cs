using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.Synchronization.Abstract;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;

namespace Integration.Synchronization
{
    class CompetitionSynchronizer : BaseSynchronizer
    {
        public CompetitionSynchronizer(IStratosphereUnitOfWork stratosphereUnitOfWork, IMapper mapper,
            ILogger<CompetitionSynchronizer> logger) : base(stratosphereUnitOfWork, mapper, logger)
        {

        }

        public SynchrnoizationResult CreateUpdateCompetition(CompetitionDto competitionDto)
        {
            

            var exisitingCompetition = StratosphereUnitOfWork.Competitions.SingleOrDefault(c => c.ExternalId == competitionDto.Id);

            if (exisitingCompetition == null)
            {
                CreateNewCompetition(competitionDto);
                return SynchrnoizationResult.CreatedNew;
            }
            else
            {
                UpdateExistingCompetition(competitionDto);
                return SynchrnoizationResult.UpdatedExisting;
            }
            
        }

        private void UpdateExistingCompetition(CompetitionDto competitionDto)
        {
            throw new NotImplementedException();
        }

        private void CreateNewCompetition(CompetitionDto competitionDto)
        {
            throw new NotImplementedException();
        }
    }
}
