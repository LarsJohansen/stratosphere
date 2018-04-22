namespace Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract
{
    public interface ICompetitionStructureSynchController
    {
        void Run(string leagueCategory, uint year);
    }
}