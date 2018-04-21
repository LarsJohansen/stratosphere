using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;

namespace Integration.FootballDataOrgApi.Mappings
{
    public class MappingsProfile : Profile 
    {
        public MappingsProfile()
        {
            CreateMap<Standing, Group>()
                .ForMember(dest => dest.LeagueCaption, opt => opt.MapFrom(src => src.Group));

        }
    }
}
