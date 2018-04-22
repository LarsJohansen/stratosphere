using Integration.FootballDataOrgApi.FootballDataDto;

namespace Integration
{
    public interface ICompetitionSetupSynchronizer
    {
        void CreateCompetitionSetup(CompetitionDto competitionDto, long competitionId);
        void UpdateExisitingCompetitionSetup(CompetitionDto competitionDto, long competitionId);
    }
}