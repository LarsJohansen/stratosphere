﻿using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Synchronization.CompetitionStructure
{
    public class CompetitionSetupSynchronizer : BaseSynchronizer, ICompetitionSetupSynchronizer
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
                StratosphereUnitOfWork.CompetitionSetups.SingleOrDefault(setup => setup.FkCompetition == competitionId);

            if (exisitingSetup == null)
            {
                Logger.LogDebug($"Could not find existing setup for {competitionDto.League}, creating new");
                CreateCompetitionSetup(competitionDto, competitionId);
                return;
            }

            var newSetup = Mapper.Map<CompetitionDto, CompetitionSetup>(competitionDto);

            exisitingSetup.NumberOfGroups = newSetup.NumberOfGroups;
            exisitingSetup.NumberOfTeams = newSetup.NumberOfTeams;
            exisitingSetup.NumberOfTeamsToPlayOff = newSetup.NumberOfTeamsToPlayOff;

        }
    }
}