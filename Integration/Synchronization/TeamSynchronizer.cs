using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.Synchronization.Abstract;
using Integration.Synchronization.Tools;
using Integration.Tools.Abstract;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.Synchronization
{
    public class TeamSynchronizer : BaseSynchronizer, ITeamSynchronizer
    {
        private readonly ILeagueTableGroupFetcher _leagueTableGroupFetcher;

        public TeamSynchronizer(IStratosphereUnitOfWork stratosphereUnitOfWork,  IMapper mapper,  ILogger<TeamSynchronizer> logger,
        ILeagueTableGroupFetcher leagueTableGroupFetcher) : base(stratosphereUnitOfWork, mapper, logger)
        {
            _leagueTableGroupFetcher = leagueTableGroupFetcher ?? throw new NullReferenceException(nameof(leagueTableGroupFetcher));
        }

        public void CreateTeams(Competition competition)
        {
            var (leagueTable, allGroupStandings) = _leagueTableGroupFetcher.GetLeagueStandings(competition.ExternalId);
          
            var exisitingTeams = StratosphereUnitOfWork.Teams.Find(t => t.FkCompetition == competition.Id)
                .Select(t => t.Name).ToList();
            var existingGroups = StratosphereUnitOfWork.Groups.Find(g => g.FkCompetition == competition.Id).ToList();

            foreach (var groupStanding in allGroupStandings)
            {
                
                foreach (var standing in groupStanding)
                {
                    var groupId = existingGroups.Where(eg => eg.Name == standing.Group).Select(eg => eg.Id).FirstOrDefault();
                    if (!exisitingTeams.Contains(standing.Team))
                    {
                        var team = Mapper.Map<Standing, Team>(standing);
                        team.FkCompetition = competition.Id;
                        team.FkGroup = groupId;
                        StratosphereUnitOfWork.Teams.Add(team);
                    }
                    
                }
            }

            StratosphereUnitOfWork.Complete();
        }
    }
}
