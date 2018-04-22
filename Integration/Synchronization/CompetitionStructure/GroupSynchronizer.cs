using System;
using System.Linq;
using AutoMapper;
using Integration.Synchronization.CompetitionStructure.Abstract;
using Integration.Synchronization.Tools;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.Synchronization.CompetitionStructure
{
    public class GroupSynchronizer : BaseSynchronizer, IGroupSynchronizer
    {
        private readonly ILeagueTableGroupFetcher _leagueTableGroupFetcher;
        public GroupSynchronizer(ILeagueTableGroupFetcher leagueTableGroupFetcher, IStratosphereUnitOfWork stratosphereUnitOfWork,
            IMapper mapper, ILogger<BaseSynchronizer> logger) : base(stratosphereUnitOfWork, mapper, logger)
        {
            _leagueTableGroupFetcher = leagueTableGroupFetcher ??
                                       throw new ArgumentNullException(nameof(leagueTableGroupFetcher));
        }

        public void CreateUpdateGroups(Competition competition)
        {
            var (leagueTable, allGroupStandings) = _leagueTableGroupFetcher.GetLeagueStandings(competition.ExternalId);
            var existingGroups = StratosphereUnitOfWork.Groups.Find(g => g.FkCompetition == competition.Id).Select(g => g.Name).ToList();
            
            Logger.LogDebug($"Found {existingGroups.Count} exisiting groups");

            foreach (var groupStanding in allGroupStandings)
            {
               
                var groupNames = groupStanding.Select(gs => gs.Group).Distinct();

                foreach (var groupName in groupNames)
                {
                    if (existingGroups.Contains(groupName))
                    {
                        continue;
                    }
                    Logger.LogDebug($"Adding group {groupName}");
                    StratosphereUnitOfWork.Groups.Add(new Group
                    {
                        Name = groupName, 
                        FkCompetition = competition.Id
                    });

                }
            }

            StratosphereUnitOfWork.Complete();
        }
    }
}
