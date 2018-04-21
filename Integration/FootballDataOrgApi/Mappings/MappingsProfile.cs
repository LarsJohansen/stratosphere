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

            CreateMap<CompetitionDto, Competition>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.League))
                .ForMember(dest => dest.CreatedAt, opt => opt.UseValue(DateTime.Now))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $"{src.Caption} {src.Year}"));


        }
    }
}
