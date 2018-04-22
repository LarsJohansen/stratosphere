namespace Integration.Synchronization.Abstract
{
    public interface ICompetitionStructureSynchController
    {
        void Run(string leagueCategory, uint year);
    }
}