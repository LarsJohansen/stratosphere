using System;
using System.Linq;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract;
using Integration.FootballDataOrgApi.Synchronization.Tools;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Synchronization.CompetitionStructure
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
            Logger.LogDebug($"Synchronizing teams");
            var (leagueTable, allGroupStandings) = _leagueTableGroupFetcher.GetLeagueStandings(competition.ExternalId);
          
            var exisitingTeams = StratosphereUnitOfWork.Teams.Find(t => t.FkCompetition == competition.Id)
              .ToList();
            var existingGroups = StratosphereUnitOfWork.Groups.Find(g => g.FkCompetition == competition.Id).ToList();


            foreach (var groupStanding in allGroupStandings)
            {
                
                foreach (var standing in groupStanding)
                {
                    var team = exisitingTeams.SingleOrDefault(t => t.Name == standing.Team);
                    var newTeam = Mapper.Map<StandingDto, Team>(standing);
                    var groupId = existingGroups.Where(eg => eg.Name == standing.Group).Select(eg => eg.Id).FirstOrDefault();
                    if (team == null)
                    {
                        SetCompetitionAndGroupId(newTeam, groupId, competition.Id);
                        StratosphereUnitOfWork.Teams.Add(newTeam);
                    }
                    else
                    {
                        SetCompetitionAndGroupId(team, groupId, competition.Id);
                        team.ExternalId = standing.TeamId;
                    }
                }
            }

            StratosphereUnitOfWork.Complete();
        }

        private void SetCompetitionAndGroupId(Team team , long groupId, long competitionId)
        {
            team.FkGroup = groupId;
            team.FkCompetition = competitionId;
        }
    }
}
