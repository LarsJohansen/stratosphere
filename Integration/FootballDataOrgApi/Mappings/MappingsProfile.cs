using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Mappings.Resolvers;
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

            CreateMap<CompetitionDto, CompetitionSetup>()
                .ForMember(dest => dest.NumberOfGroups, opt => opt.ResolveUsing<NumberOfGroupsInCompetitionResolver>());

            CreateMap<Standing, Team>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Team))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.TeamId))
                .ForMember(dest => dest.FkGroup, opt => opt.ResolveUsing<TeamGroupResolver>())
                .ForMember(dest => dest.Competition, opt => opt.Ignore())
                .ForMember(dest => dest.Group, opt => opt.Ignore());



        }
    }

}
