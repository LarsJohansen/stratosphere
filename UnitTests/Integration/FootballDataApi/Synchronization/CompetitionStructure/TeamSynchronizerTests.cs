using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Integration;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract;
using Integration.FootballDataOrgApi.Synchronization.Tools;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Persistence.Abstract;
using Persistence.Entities;

namespace UnitTests.Integration.FootballDataApi.Synchronization.CompetitionStructure
{

    //TODO: Check for correct and updated values
    [TestFixture()]
    public class TeamSynchronizerTests
    {
        private readonly Mock<ILeagueTableGroupFetcher> _mockLeagueTableGroupFetcher = new Mock<ILeagueTableGroupFetcher>();
        private Mock<IStratosphereUnitOfWork> _mockStratosUoW;
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ICompetitionSetupSynchronizer> _mockCompetitionSetupSynchronizer = new Mock<ICompetitionSetupSynchronizer>();
        private readonly Mock<ILogger<TeamSynchronizer>> _mockLogger = new Mock<ILogger<TeamSynchronizer>>();
        private ITeamSynchronizer _teamSynchronizer;

        public TeamSynchronizerTests()
        {
            var standingDtos = GetStandingDtos();
            _mockLeagueTableGroupFetcher.Setup(m => m.GetLeagueStandings(It.IsAny<long>()))
                .Returns((new LeagueTableDto(), standingDtos));
        }

        [Test]
        public void CreateTeams_NoNewTeams_AddNotCalled()
        {
            //Arrange 
            var exisitingTeams = new List<Team>();
            var exisitinGroups = new List<Group>
            {
                new Group {FkCompetition = 12, Name = "A"},
                new Group {FkCompetition = 12, Name = "B"},
                new Group {FkCompetition = 12, Name = "C"},
                new Group {FkCompetition = 12, Name = "D"}

            };

            foreach (var standingDtoList in GetStandingDtos())
            {
                foreach (var standingDto in standingDtoList)
                {
                    var team = exisitingTeams.SingleOrDefault(t => t.Name == standingDto.Team);

                    if (team == null)
                    {
                        exisitingTeams.Add(new Team{ Name = standingDto.Team, ExternalId = standingDto.TeamId});
                    }
                }
            }



            _mockStratosUoW = new Mock<IStratosphereUnitOfWork>();
            _mockStratosUoW.Setup(m => m.Teams.Find(It.IsAny<Expression<Func<Team, bool>>>()))
                .Returns(exisitingTeams);
            _mockStratosUoW.Setup(m => m.Groups.Find(It.IsAny<Expression<Func<Group, bool>>>()))
                .Returns(exisitinGroups);
            _mockMapper.Setup(m => m.Map<StandingDto, Team>(It.IsAny<StandingDto>()))
                .Returns((StandingDto standing) => new Team { Name = standing.Team, ExternalId = standing.TeamId});
                
                    
                
            //Act 
            _teamSynchronizer = new TeamSynchronizer(_mockStratosUoW.Object, _mockMapper.Object, _mockLogger.Object, 
                _mockLeagueTableGroupFetcher.Object);
            _teamSynchronizer.CreateTeams(new Competition{ Id = 66 });

            //Assert 
            _mockStratosUoW.Verify(m => m.Teams.Add(It.IsAny<Team>()), Times.Never);
            _mockStratosUoW.Verify(m => m.Complete(), Times.Once);
        }

        [Test]
        public void CreateTeams_6NewTeams_AddCalled6Times()
        {
            //Arrange 
            var exisitingTeams = new List<Team>();
            var exisitinGroups = new List<Group>
            {
                new Group {FkCompetition = 12, Name = "A"},
                new Group {FkCompetition = 12, Name = "B"},
                new Group {FkCompetition = 12, Name = "C"},
                new Group {FkCompetition = 12, Name = "D"}

            };

          

            _mockStratosUoW = new Mock<IStratosphereUnitOfWork>();
            _mockStratosUoW.Setup(m => m.Teams.Find(It.IsAny<Expression<Func<Team, bool>>>()))
                .Returns(exisitingTeams);
            _mockStratosUoW.Setup(m => m.Groups.Find(It.IsAny<Expression<Func<Group, bool>>>()))
                .Returns(exisitinGroups);
            _mockMapper.Setup(m => m.Map<StandingDto, Team>(It.IsAny<StandingDto>()))
                .Returns((StandingDto standing) => new Team { Name = standing.Team, ExternalId = standing.TeamId });



            //Act 
            _teamSynchronizer = new TeamSynchronizer(_mockStratosUoW.Object, _mockMapper.Object, _mockLogger.Object,
                _mockLeagueTableGroupFetcher.Object);
            _teamSynchronizer.CreateTeams(new Competition { Id = 66 });

            //Assert 
            _mockStratosUoW.Verify(m => m.Teams.Add(It.IsAny<Team>()), Times.Exactly(6));
            _mockStratosUoW.Verify(m => m.Complete(), Times.Once);
        }


        private List<List<StandingDto>> GetStandingDtos()
        {
            return new List<List<StandingDto>>
            {
                new List<StandingDto>
                {
                    new StandingDto {Group = "A", Team = "Team1"},
                    new StandingDto {Group = "B", Team = "Team2"}
                },
                new List<StandingDto>
                {
                    new StandingDto {Group = "C", Team = "Team4"},
                    new StandingDto {Group = "C", Team = "Team5"}
                },
                new List<StandingDto>
                {
                    new StandingDto {Group = "D", Team = "Team6"},
                    new StandingDto {Group = "D", Team = "Team7"}
                }

            };
        }
    }
}
