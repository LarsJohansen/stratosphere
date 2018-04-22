using Persistence.Entities;

namespace Integration.Synchronization.CompetitionStructure.Abstract
{
    public interface ITeamSynchronizer
    {
        void CreateTeams(Competition competition);
    }
}