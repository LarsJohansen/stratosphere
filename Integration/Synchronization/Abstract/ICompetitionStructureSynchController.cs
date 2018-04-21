namespace Integration.Synchronization
{
    public interface ICompetitionStructureSynchController
    {
        void Run(string leagueCategory, uint year);
    }
}