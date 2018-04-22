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
    public class CompetitionSetupSynchronizer : BaseSynchronizer
    {
        public CompetitionSetupSynchronizer(IStratosphereUnitOfWork stratosphereUnitOfWork, IMapper mapper, 
            ILogger<CompetitionSetupSynchronizer> logger) : base(stratosphereUnitOfWork, mapper, logger)
        {

        }

        public void CreateCompetitionSetup(CompetitionDto competitionDto, long competitionId)
        {
            var competitionSetup = Mapper.Map<CompetitionDto, CompetitionSetup>(competitionDto);
            competitionSetup.FkCompetition = competitionId;
            StratosphereUnitOfWork.CompetitionSetups.Add(competitionSetup);
        }

        public void UpdateExisitingCompetitionSetup(CompetitionDto competitionDto, long competitionId)
        {
            var exisitingSetup =
                StratosphereUnitOfWork.CompetitionSetups.Find(setup => setup.FkCompetition == competitionId);

        }
    }
}
