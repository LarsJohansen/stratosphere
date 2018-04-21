using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Mappings
{
    public class MappingsProfile : Profile 
    {
        public MappingsProfile()
        {
            CreateMap<Standing, Group>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Group));

        }
    }
}
