using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.Synchronization.Tools;
using Persistence.Abstract;
using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Mappings.Resolvers
{
    public class TeamGroupResolver : IValueResolver<Standing, Team, long>
    {

        private readonly IStratosphereUnitOfWork _stratosphereUnitOfWork;

        public TeamGroupResolver(IStratosphereUnitOfWork stratosphereUnitOfWork)
        {
            _stratosphereUnitOfWork =
                stratosphereUnitOfWork ?? throw new ArgumentNullException(nameof(stratosphereUnitOfWork));

        }


        public long Resolve(Standing source, Team destination, long destMember, ResolutionContext context)
        {
            return 0;
        }
    }
}