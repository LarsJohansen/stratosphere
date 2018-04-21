using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.Synchronization.Abstract;
using Persistence.Entities;

namespace Integration.Synchronization
{
    public class GroupSynchronizer : BaseSynchronizer, IGroupSynchronizer
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
