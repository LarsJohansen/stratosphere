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
            _mockStratosUoW.Setup(m => m.CompetitionSetups.Add(It.IsAny<CompetitionSetup>())).Verifiable();
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
        public void UpdateExistingCompetitionSetup_WithoutExistingCompetitionSetup_CreateCompetitionSetupInvoked()
        {
            //Arrange 
            ResetMocks();
            CompetitionSetup existingCompetitionSetup = null;
            var newCompetitionSetup = new CompetitionSetup();
            var mockStatosUoW = new Mock<IStratosphereUnitOfWork>();
            mockStatosUoW.Setup(m =>
                    m.CompetitionSetups.SingleOrDefault(It.IsAny<Expression<Func<CompetitionSetup, bool>>>()))
                .Returns(existingCompetitionSetup);


            _mockMapper.Setup(m => m.Map<CompetitionDto, CompetitionSetup>(It.IsAny<CompetitionDto>()))
                .Returns(newCompetitionSetup);
            var competitionId = 123;

            //Act 
            //TODO:  _mockStratosUoW.ResetCalls(); does not seem to reset verify counter, hence new ... why?

            _competitionSetupSynchronizer = new CompetitionSetupSynchronizer(mockStatosUoW.Object, _mockMapper.Object,
                _mockLogger.Object);
            _competitionSetupSynchronizer.UpdateExisitingCompetitionSetup(new CompetitionDto(), competitionId);

            //Assert
            Assert.AreEqual(competitionId, newCompetitionSetup.FkCompetition);
            _mockStratosUoW.Verify(m => m.CompetitionSetups.Add(It.IsAny<CompetitionSetup>()), Times.Once);

        }

        private void ResetMocks()
        {

            _mockMapper.Reset();
            _mockStratosUoW.Reset();
        
            _mockMapper.ResetCalls();
            _mockStratosUoW.ResetCalls();
     

        }
    }
}
