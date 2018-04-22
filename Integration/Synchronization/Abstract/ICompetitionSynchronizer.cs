using Integration.FootballDataOrgApi.FootballDataDto;

namespace Integration.Synchronization.Abstract
{
    public interface ICompetitionSynchronizer
    {
        SynchrnoizationResult CreateUpdateCompetition(CompetitionDto competitionDto);
    }
}