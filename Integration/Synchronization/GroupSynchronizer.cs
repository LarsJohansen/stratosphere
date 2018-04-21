using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.Synchronization.Abstract;

namespace Integration.Synchronization
{
    public class GroupSynchronizer : BaseSyncronizer, IGroupSynchronizer
    {
       
        public GroupSynchronizer(IMapper mapper) : base(mapper)
        {
           
        }

        public Group GetGroupFromStanding(Standing standing)
        {
            return Mapper.Map<Standing, Group>(standing);
        }
    }
}
