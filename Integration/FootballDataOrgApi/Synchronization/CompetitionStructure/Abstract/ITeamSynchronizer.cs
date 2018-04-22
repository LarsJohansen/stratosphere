using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract
{
    public interface ITeamSynchronizer
    {
        void CreateTeams(Competition competition);
    }
}