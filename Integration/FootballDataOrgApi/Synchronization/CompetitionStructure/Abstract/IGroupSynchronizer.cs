using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract
{
    public interface IGroupSynchronizer
    {
        void CreateUpdateGroups(Competition competition);
    }
}