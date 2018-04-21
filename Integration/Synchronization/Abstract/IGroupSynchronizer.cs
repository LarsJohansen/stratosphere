using Integration.FootballDataOrgApi.FootballDataDto;

namespace Integration.Synchronization
{
    public interface IGroupSynchronizer
    {
        Group GetGroupFromStanding(Standing standing);
    }
}