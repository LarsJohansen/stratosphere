using System.Collections.Generic;
using System.Reflection;
using Integration.FootballDataOrgApi.FootballDataDto;

namespace Integration.Synchronization.Tools
{
    public interface ILeagueTableGroupFetcher
    {
        (LeagueTable, IEnumerable<IEnumerable<Standing>>) GetLeagueStandings(long competitionId);
    }
}