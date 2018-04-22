using Integration.FootballDataOrgApi.FootballDataDto;
using Persistence.Entities;

namespace Integration.Synchronization.Abstract
{
    public interface ICompetitionSynchronizer
    {
        Competition CreateUpdateCompetition(CompetitionDto competitionDto);
    }
}