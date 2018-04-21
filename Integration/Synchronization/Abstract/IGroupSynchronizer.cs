using Integration.FootballDataOrgApi.FootballDataDto;
using Persistence.Entities;

namespace Integration.Synchronization.Abstract
{
    public interface IGroupSynchronizer
    {
        Group GetGroupFromStanding(Standing standing);
    }
}