using Integration.FootballDataOrgApi.FootballDataDto;
using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract
{
    public interface ICompetitionSynchronizer
    {
        Competition CreateUpdateCompetition(CompetitionDto competitionDto);
    }
}