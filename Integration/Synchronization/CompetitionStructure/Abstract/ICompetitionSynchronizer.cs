using Integration.FootballDataOrgApi.FootballDataDto;
using Persistence.Entities;

namespace Integration.Synchronization.CompetitionStructure.Abstract
{
    public interface ICompetitionSynchronizer
    {
        Competition CreateUpdateCompetition(CompetitionDto competitionDto);
    }
}