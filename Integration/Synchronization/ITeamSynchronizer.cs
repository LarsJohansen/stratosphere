using Persistence.Entities;

namespace Integration.Synchronization
{
    public interface ITeamSynchronizer
    {
        void CreateTeams(Competition competition);
    }
}