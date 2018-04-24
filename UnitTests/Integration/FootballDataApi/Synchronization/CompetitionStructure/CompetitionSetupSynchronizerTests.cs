using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Integration;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Persistence.Abstract;
using Persistence.Entities;

namespace UnitTests.Integration.FootballDataApi.Synchronization.CompetitionStructure
{
    [TestFixture()]
    public class CompetitionSetupSynchronizerTests
    {
        private  Mock<IStratosphereUnitOfWork> _mockStratosUoW = new Mock<IStratosphereUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ILogger<CompetitionSetupSynchronizer>> _mockLogger = new Mock<ILogger<CompetitionSetupSynchronizer>>();
        private ICompetitionSetupSynchronizer _competitionSetupSynchronizer;

        public CompetitionSetupSynchronizerTests()
        {
            _mockStratosUoW.Setup(m => m.CompetitionSetups.Add(It.IsAny<CompetitionSetup>()));
            _competitionSetupSynchronizer = new CompetitionSetupSynchronizer(_mockStratosUoW.Object, _mockMapper.Object,
                _mockLogger.Object);
            
        }

        [Test]
        public void CreateCompetitionSetup_WithNewCompetition_NewCompetitionAdded()
        {
            ResetMocks();
            //Arrange 
            var competitionSetup = new CompetitionSetup { };
            var competitionId = 23;

            _mockMapper.Setup(m => m.Map<CompetitionDto, CompetitionSetup>(It.IsAny<CompetitionDto>()))
                .Returns(competitionSetup);

            //Act 
            _competitionSetupSynchronizer.CreateCompetitionSetup(new CompetitionDto(), competitionId);

            //Assert
            Assert.AreEqual(competitionId, competitionSetup.FkCompetition);
            _mockStratosUoW.Verify(m => m.CompetitionSetups.Add(It.IsAny<CompetitionSetup>()), Times.Once);
        }

        [Test]
        public void UpdateExistingCompetitionSetup_NoExistingCompetitionSetup_CompetitionSetupAdded()
        {
            //Arrange 
            ResetMocks();
            _mockStratosUoW  = new Mock<IStratosphereUnitOfWork>();
            _mockStratosUoW.Setup(m => m.CompetitionSetups.Add(It.IsAny<CompetitionSetup>()));

            CompetitionSetup existingCompetitionSetup = null;

            var newCompetitionSetup = new CompetitionSetup(); 
            _mockMapper.Setup(m => m.Map<CompetitionDto, CompetitionSetup>(It.IsAny<CompetitionDto>()))
                .Returns(newCompetitionSetup);

            var competitionId = 123;

            //Act 
           _competitionSetupSynchronizer = new CompetitionSetupSynchronizer(_mockStratosUoW.Object, _mockMapper.Object,
                _mockLogger.Object);
            _competitionSetupSynchronizer.UpdateExisitingCompetitionSetup(new CompetitionDto(), competitionId);

            //Assert
            Assert.AreEqual(competitionId, competitionId);
            _mockStratosUoW.Verify(m => m.CompetitionSetups.Add(It.IsAny<CompetitionSetup>()), Times.Once);

        }

        [Test]
        public void UpdateExistingCompetitionSetup_HasExistingCompetitionSetup_CompetitonSetupUpdatedNotAdded()
        {
            //Arrange 
            _mockStratosUoW = new Mock<IStratosphereUnitOfWork>();
            var existingCompetitionSetup = new CompetitionSetup{ NumberOfGroups = 1, NumberOfTeams = 1, NumberOfTeamsToPlayOff = 1, FkCompetition = 123};
            _mockStratosUoW.Setup(m =>
                    m.CompetitionSetups.SingleOrDefault(It.IsAny<Expression<Func<CompetitionSetup, bool>>>()))
                .Returns(existingCompetitionSetup);
        
            var newCompetitionSetup = new CompetitionSetup{ NumberOfGroups = 11, NumberOfTeams = 12, NumberOfTeamsToPlayOff = 13};
            _mockMapper.Setup(m => m.Map<CompetitionDto, CompetitionSetup>(It.IsAny<CompetitionDto>()))
                .Returns(newCompetitionSetup);
            var competitionId = 123;

            //Act 
            _competitionSetupSynchronizer = new CompetitionSetupSynchronizer(_mockStratosUoW.Object, _mockMapper.Object,
                _mockLogger.Object);
            _competitionSetupSynchronizer.UpdateExisitingCompetitionSetup(new CompetitionDto(), competitionId);

            //Assert
            Assert.AreEqual(newCompetitionSetup.NumberOfTeams, existingCompetitionSetup.NumberOfTeams);
            Assert.AreEqual(newCompetitionSetup.NumberOfTeamsToPlayOff, existingCompetitionSetup.NumberOfTeamsToPlayOff);
            Assert.AreEqual(newCompetitionSetup.NumberOfGroups , existingCompetitionSetup.NumberOfGroups);
            _mockStratosUoW.Verify(m => m.CompetitionSetups.Add(It.IsAny<CompetitionSetup>()), Times.Never);

        }

        private void ResetMocks()
        {

            _mockMapper.Reset();
            _mockStratosUoW.Reset();
        

        }
    }
}
