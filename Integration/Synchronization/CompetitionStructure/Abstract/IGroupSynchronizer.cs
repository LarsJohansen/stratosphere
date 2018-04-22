using Persistence.Entities;

namespace Integration.Synchronization.CompetitionStructure.Abstract
{
    public interface IGroupSynchronizer
    {
        void CreateUpdateGroups(Competition competition);
    }
}