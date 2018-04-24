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
using NUnit.Framework.Internal;
using Persistence.Abstract;
using Persistence.Entities;

namespace UnitTests.Integration.FootballDataApi.Synchronization.CompetitionStructure
{

    [TestFixture()]
    public class GroupSynchronizerTests
    {
        private readonly Mock<ILeagueTableGroupFetcher> _mockLeagueTableGroupFetcher = new Mock<ILeagueTableGroupFetcher>();
        private Mock<IStratosphereUnitOfWork> _mockStratosUoW;
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ICompetitionSetupSynchronizer> _mockCompetitionSetupSynchronizer = new Mock<ICompetitionSetupSynchronizer>();
        private readonly Mock<ILogger<CompetitionSynchronizer>> _mockLogger = new Mock<ILogger<CompetitionSynchronizer>>();
        private IGroupSynchronizer _groupSynchronizer;

        public GroupSynchronizerTests()
        {
            var standingDtos = GetStandingDtos();
            _mockLeagueTableGroupFetcher.Setup(m => m.GetLeagueStandings(It.IsAny<long>()))
                .Returns((new LeagueTableDto(), standingDtos));
        }

        [Test]
        public void GroupSynchronizer_WithNoNewGroups_AddNeverCalled()
        {
            //Arrange 
            _mockStratosUoW = new Mock<IStratosphereUnitOfWork>();

            var exisitingGroups = new List<Group>();

            foreach (var standingsDtoList in GetStandingDtos())
            {
                foreach (var standingDto in standingsDtoList)
                {
                    var group = exisitingGroups.FirstOrDefault(g => g.Name == standingDto.Group);
                    if (group == null)
                    {
                        exisitingGroups.Add(new Group{ Name = standingDto.Group});
                    }
                   
                }
            }

            _mockStratosUoW.Setup(m => m.Groups.Find(It.IsAny<Expression<Func<Group, bool>>>()))
                .Returns(exisitingGroups);
            //Act 
            _groupSynchronizer = new GroupSynchronizer(_mockLeagueTableGroupFetcher.Object, _mockStratosUoW.Object,_mockMapper.Object, _mockLogger.Object);
            _groupSynchronizer.CreateUpdateGroups(new Competition());

            //Assert
            _mockStratosUoW.Verify(m => m.Groups.Add(It.IsAny<Group>()), Times.Never);
        }

        [Test]
        public void GroupSynchronizer_WithNoGroups_AddCalled4Times()
        {
            //Arrange 
            _mockStratosUoW = new Mock<IStratosphereUnitOfWork>();
            
            _mockStratosUoW.Setup(m => m.Groups.Find(It.IsAny<Expression<Func<Group, bool>>>()))
                .Returns(new List<Group>());

            //Act 
            _groupSynchronizer = new GroupSynchronizer(_mockLeagueTableGroupFetcher.Object, _mockStratosUoW.Object, _mockMapper.Object, _mockLogger.Object);
            _groupSynchronizer.CreateUpdateGroups(new Competition());


            //Assert
            _mockStratosUoW.Verify(m => m.Groups.Add(It.IsAny<Group>()), Times.Exactly(4));
            _mockStratosUoW.Verify(m => m.Complete(), Times.Once);
        }

        private List<List<StandingDto>> GetStandingDtos()
        {
            return new List<List<StandingDto>>
            {
                new List<StandingDto>
                {
                    new StandingDto {Group = "A"},
                    new StandingDto {Group = "B"}
                },
                new List<StandingDto>
                {
                    new StandingDto {Group = "C"},
                    new StandingDto {Group = "C"}
                },
                new List<StandingDto>
                {
                    new StandingDto {Group = "D"},
                    new StandingDto {Group = "D"}
                }

            };
        }
    
    }
}
