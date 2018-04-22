using System.Collections.Generic;
using System.Reflection;
using Integration.FootballDataOrgApi.FootballDataDto;

namespace Integration.Synchronization.Tools
{
    public interface ILeagueTableGroupFetcher
    {
        (LeagueTableDto, IEnumerable<IEnumerable<StandingDto>>) GetLeagueStandings(long competitionId);
    }
}