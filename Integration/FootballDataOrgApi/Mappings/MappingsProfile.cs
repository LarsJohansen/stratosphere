using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Mappings.Resolvers;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Persistence.Entities;

namespace Integration.FootballDataOrgApi.Mappings
{
    public class MappingsProfile : Profile 
    {
        public MappingsProfile()
        {
            CreateMap<StandingDto, Group>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Group));

            CreateMap<CompetitionDto, Competition>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Caption} {src.Year}"))
                .ForMember(dest => dest.League, opt => opt.MapFrom(src => src.League))
                .ForMember(dest => dest.CreatedAt, opt => opt.UseValue(DateTime.Now))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $"{src.Caption} {src.Year}"))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CompetitionDto, CompetitionSetup>()
                .ForMember(dest => dest.NumberOfGroups, opt => opt.ResolveUsing<NumberOfGroupsInCompetitionResolver>())
                .ForMember(dest => dest.NumberOfTeamsToPlayOff,
                    opt => opt.ResolveUsing<NumberOfTeamsToPlayOffResolver>());

            CreateMap<StandingDto, Team>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Team))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.TeamId))
                .ForMember(dest => dest.Competition, opt => opt.Ignore())
                .ForMember(dest => dest.Group, opt => opt.Ignore());



        }
    }

}
