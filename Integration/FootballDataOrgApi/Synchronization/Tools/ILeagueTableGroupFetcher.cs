using System.Collections.Generic;
using Integration.FootballDataOrgApi.FootballDataDto;

namespace Integration.FootballDataOrgApi.Synchronization.Tools
{
    public interface ILeagueTableGroupFetcher
    {
        (LeagueTableDto, IEnumerable<IEnumerable<StandingDto>>) GetLeagueStandings(long competitionId);
    }
}