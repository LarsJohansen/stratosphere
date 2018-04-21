using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.Synchronization.Abstract;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.Synchronization
{
    public class GroupSynchronizer : BaseSynchronizer, IGroupSynchronizer
    {
       
        public GroupSynchronizer(IStratosphereUnitOfWork stratosphereUnitOfWork,IMapper mapper, ILogger<GroupSynchronizer> logger) : base(stratosphereUnitOfWork, mapper, logger)
        {
           
        }

        public Group GetGroupFromStanding(Standing standing)
        {
            
            return Mapper.Map<Standing, Group>(standing);
        }
    }
}
