using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.Synchronization.Abstract;
using Integration.Synchronization.Tools;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.Synchronization
{
    public class GroupSynchronizer : BaseSynchronizer, IGroupSynchronizer
    {
        private readonly ILeagueTableGroupFetcher _leagueTableGroupFetcher;
        public GroupSynchronizer(ILeagueTableGroupFetcher leagueTableGroupFetcher, IStratosphereUnitOfWork stratosphereUnitOfWork,IMapper mapper, ILogger<GroupSynchronizer> logger) : base(stratosphereUnitOfWork, mapper, logger)
        {
            _leagueTableGroupFetcher = leagueTableGroupFetcher ??
                                       throw new ArgumentNullException(nameof(leagueTableGroupFetcher));
        }

        public void CreateUpdateGroups(Competition competition)
        {
            var (leagueTable, allGroupStandings) = _leagueTableGroupFetcher.GetLeagueStandings(competition.ExternalId);
            var existingGroups = StratosphereUnitOfWork.Groups.Find(g => g.FkCompetition == competition.Id).Select(g => g.Name).ToList();
            

            foreach (var groupStanding in allGroupStandings)
            {
               
                var groupNames = groupStanding.Select(gs => gs.Group).Distinct();

                foreach (var groupName in groupNames)
                {
                    if (existingGroups.Contains(groupName))
                    {
                        continue;
                    }
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
